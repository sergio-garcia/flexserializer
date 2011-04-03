// Copyright 2011 Ginx. All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
//
//   1. Redistributions of source code must retain the above copyright notice, this list of
//      conditions and the following disclaimer.
//
//   2. Redistributions in binary form must reproduce the above copyright notice, this list
//      of conditions and the following disclaimer in the documentation and/or other materials
//      provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY Ginx ``AS IS'' AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// The views and conclusions contained in the software and documentation are those of the
// authors and should not be interpreted as representing official policies, either expressed
// or implied, of Ginx.
package br.com.ginx.flexserializer
{
	import flash.utils.describeType;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.utils.ObjectUtil;
	import mx.utils.object_proxy;
	
	public class DataContractSerializer
	{	
		private var XSI:String = "http://www.w3.org/2001/XMLSchema-instance";
		private var MS:String = "http://schemas.microsoft.com/2003/10/Serialization/";
		private var DC:String = "http://schemas.datacontract.org/2004/07/";
		
		private var remoteToNative : Object;
		private var nativeToRemote : Object;
		
		private var serializerType:String;
		private var serializerNamespace:String;
		
		public function DataContractSerializer(type:Class)
		{
			var descriptor:XML = describeType(type);
			
			remoteToNative = new Object();
			nativeToRemote = new Object();
			
			serializerType      = getClassName(descriptor.@name);
			serializerNamespace = getNamespace(descriptor.@name);
		}
		
		public function addNamespace(native:String, remote:String) : void
		{
			remoteToNative[remote] = native;
			nativeToRemote[native] = remote;
		}
		
		private function getRemoteNamespace(native:String) : String
		{
			var remote:String = nativeToRemote[native];
			
			if (!remote)
			{
				remote = DC + native;
			}
			
			return remote;
		}
		
		private static function getClassName(name:String) : String
		{
			if (name.indexOf("::") > 0)
			{
				return name.split("::")[1];
			}
			
			return name;
		}
		
		private static function getNamespace(name:String) : String
		{
			if (name.indexOf("::") > 0)
			{
				return name.split("::")[0];
			}
			
			return "";
		}
		
		public function serialize(source:Object) : String
		{
			var descriptor:XML = describeType(source);
			
			var sourceType:String      = getClassName(descriptor.@name);
			var sourceNamespace:String = getNamespace(descriptor.@name);
			
			var writer:XmlWriter=new XmlWriter();
			
			var remoteSerializerNamespace:String = getRemoteNamespace(serializerNamespace);
			
			writer.writeStartElement(serializerType);
			writer.writeNamespace(remoteSerializerNamespace, "");
			writer.writeNamespace(XSI, "xsi");
			
			if (serializerType != sourceType)
			{				
				writer.writeAttribute("xsi:type", sourceType);
			}
						
			writeMembers(writer, descriptor, source);
			writer.writeEndElement(serializerType);
			
			return writer.toString();
		}
			
		public function deserialize(data:String):Object
		{
			var xml:XML = new XML(data);
			
			var objectType:String = serializerType;
			
			if (xml.attribute(new QName(XSI, "type"))[0])
			{
				objectType = xml.attribute(new QName(XSI, "type"))[0];
			}
			
			var objectClass:Class = flash.utils.getDefinitionByName(serializerNamespace + "." + objectType) as Class;
			var object:Object = new objectClass();
			
			readMembers(xml, object);
			
			return object;
		}
		
		private function writeMembers(writer:XmlWriter, descriptor:XML, source:Object) : void {
			var property:XML;	
			
			var properties:ArrayCollection = new ArrayCollection();
			
			for each(property in descriptor..variable)
			{	
				var variable:Object = new Object();
				variable.name = property.@name;
				variable.property = property;
				properties.addItem(variable);
			}
			
			for each(property in descriptor..accessor)
			{
				if (property.@access=="readonly")
				{
					continue;
				}
				
				var accessor:Object = new Object();
				accessor.name = property.@name;
				accessor.property = property;
				properties.addItem(accessor);
			}
			
			// .net datacontract requires fields to be sorted
			var sort:Sort = new Sort();
			sort.fields = [ new SortField("name") ];
			
			properties.sort = sort;
			properties.refresh();
			
			for each(var p:Object in properties)
			{
				writeMember(writer, p.property, source);
			}
		}
		
		private function writeMember(writer:XmlWriter, property:XML, source:Object) : void
		{
			var propertyValue:Object = source[property.@name];
			
			if (propertyValue != null)
			{
				if (propertyValue is Array)
				{
					writer.writeStartElement(property.@name);
					
					for (var index:Object in propertyValue)
					{	
						var itemDescriptor:XML = describeType(propertyValue[index]);
						
						var type:String= getClassName(itemDescriptor.@name);
						var baseClassName:String=getClassName(itemDescriptor.@base);
						
						writer.writeStartElement(type);
						writeMembers(writer, itemDescriptor, propertyValue[index]);
						writer.writeEndElement(type);				
					}
					
					writer.writeEndElement(property.@name);
				}
				else if (ObjectUtil.isSimple(propertyValue))
				{
					writer.writeNameValueNode(property.@name, propertyValue);
				}
				else
				{
					writer.writeStartElement(property.@name);
					writeMembers(writer, describeType(propertyValue), propertyValue);
					writer.writeEndElement(property.@name);
				}
			}
		}
		
		private function getPropertyDescriptor(propertyName:String, descriptor:XML) : XML {
			var property:XML;	
			
			for each(property in descriptor..variable)
			{				
				if (property.@name == propertyName)
				{
					return property;
				}
			}
			
			for each(property in descriptor..accessor)
			{
				if (property.@name == propertyName)
				{
					return property;
				}
			}
			
			return null;
		}
		
		private function readMembers(xml:XML, object:Object) : void
		{
			var descriptor:XML = describeType(object);
			
			for each(var node:XML in xml.children())
			{
				readMember(node, descriptor, object);				
			}
		}
		
		private function readMember(node:XML, descriptor:XML, object:Object) : void
		{
			var propertyName:String = getClassName(node.name());
			var propertyType:String = getTypeFromDescriptor(propertyName, descriptor);
			
			if (propertyType != null)
			{
				var propertyClass:Class = flash.utils.getDefinitionByName(propertyType) as Class;						
				object[propertyName] = readMemberValue(propertyClass, propertyName, node, descriptor);	
			}
		}
		
		private function readMemberValue(propertyClass:Class, propertyName:String, node:XML, descriptor:XML) : Object
		{
			var propertyObject:Object = new propertyClass();
			
			if (propertyObject is Array)
			{
				var propertyDescriptor: XML = getPropertyDescriptor(propertyName, descriptor);
				
				var elementType:String = getArrayElementType(propertyDescriptor);
				
				var children:XMLList = node.children();
				
				for (var index:Object in children)
				{
					var childName:String   = getClassName(children[index].name());
					var childType:String   = childName;
					var childClass:Class   = flash.utils.getDefinitionByName(serializerNamespace + "." + childType) as Class;
					
					var childObject:Object = readMemberValue(childClass, childName, children[index], propertyDescriptor);
					propertyObject[index] = childObject;
				}
			}
			else if (propertyObject is Date)
			{
				propertyObject = Utils.stringToDate(node.toString());
			}
			else if (propertyObject is Boolean)
			{
				propertyObject = node.toString() == "true";
			}
			else if (ObjectUtil.isSimple(propertyObject))
			{
				propertyObject = new propertyClass(node.toString());
			}	
			else
			{
				if (node.attribute(new QName(XSI, "nil"))[0])
				{
					propertyObject = null;
				}
				else
				{
					readMembers(node, propertyObject);
				}
			}
			
			return propertyObject;
		}
		
		/**
		 * Checks if property with given name exists in class and returns property type.
		 * Returns null if class has no property with that name.
		 * 
		 * @param name property name
		 * @return 
		 * 
		 */
		private function getTypeFromDescriptor(name:String, descriptor:XML) : String
		{
			for each(var property:XML in descriptor.elements("variable"))
			{
				if (property.@name==name)
				{
					return (String(property.@type));
				}
			}
			
			for each(var accessor:XML in descriptor.elements("accessor"))
			{
				if (accessor.@name==name)
				{
					return (String(accessor.@type));
				}
			}
			
			return null;
		}		
		
		/**
		 * Reads [RemoteType] user defined metadata on property
		 * 
		 */
		private function getRemoteType(propertyMetadataList:XMLList):String
		{
			var remoteType:String;
			
			for each (var metadata:XML in propertyMetadataList)
			{
				if (metadata.@name=="RemoteType")
				{
					for each (var arg:XML in metadata.elements("arg"))
					{
						if (arg.@key=="alias")
						{
							remoteType=arg.@value;							
						}
					}
				}
			}
			
			return remoteType;
		}
		
		private function getArrayElementType(descriptor:XML):String
		{
			for each(var metadata:XML in descriptor.elements("metadata"))
			{
				if (metadata.@name == "ArrayElementType")
				{
					return metadata.arg.@value;
				}
			}
			
			return "Object";
		}
	}
}