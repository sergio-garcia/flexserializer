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
	import mx.formatters.DateFormatter;

	public final class Utils
	{	public static function dateTimeToString(date:Date):String
		{
			var formatter:DateFormatter = new DateFormatter();
			
			formatter.formatString = "YYY-MM-DDTJJ:NN:SS";
			
			return formatter.format(date);
		}
		
		public static function stringToDate(value:String):Date
		{
			var dateStr:String = value;
			dateStr = dateStr.replace(/\-/g, "/");
			dateStr = dateStr.replace("T", " ");
			dateStr = dateStr.replace("Z", " GMT-0000");
			return new Date(Date.parse(dateStr));
		}
		
		public static function escapeXmlString(text:String) : String
		{
			text = text.replace(/&/g, '\&amp;');
			text = text.replace(/</g, '&lt;');
			text = text.replace(/>/g, '&gt;');
			
			return text;
		}
	}
}