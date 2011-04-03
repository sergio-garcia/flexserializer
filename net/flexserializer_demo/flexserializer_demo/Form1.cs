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

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace flexserializer_demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void serializeCustomer_Click(object sender, EventArgs e)
        {
            var customer = new Customer
                               {
                                   FirstName = "Sergio",
                                   LastName = "Garcia",
                                   BirthDate = new DateTime(1984, 9, 13)
                               };

            var serializer = new DataContractSerializer(typeof(Customer));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, customer);
                objectData.Text = Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        private void deserializeCustomer_Click(object sender, EventArgs e)
        {
            var serializer = new DataContractSerializer(typeof(Customer));

            var reader = XmlReader.Create(new StringReader(objectData.Text));

            try
            {
                var customer = (Customer) serializer.ReadObject(reader);
                var msg = "== Customer:\nName: " + customer.FirstName + " " + customer.LastName + "\nBirth Date: " + customer.BirthDate;
                MessageBox.Show(msg);
            }
            catch (Exception)
            {
                MessageBox.Show("Object couldn't be deserialized.\nCheck you input data.");
            }
        }

        private void serializePreferredCustomer_Click(object sender, EventArgs e)
        {
            var customer = new PreferredCustomer()
                               {
                                   FirstName = "Sergio",
                                   LastName = "Garcia",
                                   BirthDate = new DateTime(1984, 9, 13),
                                   Seller = "Jack"
                               };

            var serializer = new DataContractSerializer(typeof(Customer), new [] {typeof(PreferredCustomer)});

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, customer);
                objectData.Text = Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        private void deserializePreferredCustomer_Click(object sender, EventArgs e)
        {
            var serializer = new DataContractSerializer(typeof(Customer), new[] { typeof(PreferredCustomer) });

            var reader = XmlReader.Create(new StringReader(objectData.Text));

            try
            {
                var customer = (PreferredCustomer)serializer.ReadObject(reader);
                var msg = "== Customer:\nName: " + customer.FirstName + " " + customer.LastName + "\nBirth Date: " + customer.BirthDate + "\nSeller: " + customer.Seller;
                MessageBox.Show(msg);
            }
            catch (Exception)
            {
                MessageBox.Show("Object couldn't be deserialized.\nCheck you input data.");
            }
        }

        private void serializeInvoice_Click(object sender, EventArgs e)
        {
            var invoice = new Invoice
                              {
                                  Customer = new Customer
                                                 {
                                                     FirstName = "Sergio",
                                                     LastName = "Garcia",
                                                     BirthDate = new DateTime(1984, 9, 13)
                                                 },
                                  Products = new List<Product>
                                                 {
                                                     new Product
                                                         {
                                                             Name = "Laser TV",
                                                             Price = 9999.99M
                                                         },
                                                     new Product
                                                         {
                                                             Name = "Quantum Computer",
                                                             Price = 7777.99M
                                                         }
                                                 }
                              };

            var serializer = new DataContractSerializer(typeof(Invoice));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, invoice);
                objectData.Text = Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        private void deserializeInvoice_Click(object sender, EventArgs e)
        {
            var serializer = new DataContractSerializer(typeof(Invoice));

            var reader = XmlReader.Create(new StringReader(objectData.Text));

            try
            {
                var invoice = (Invoice)serializer.ReadObject(reader);
                var msg = "== Invoice Customer:\nName: " + invoice.Customer.FirstName + " " + invoice.Customer.LastName + "\nBirth Date: " + invoice.Customer.BirthDate;
                
                msg += "\n\n== Products";
                
                for (int index = 0; index < invoice.Products.Count; index++)
                {
                    msg += "\n== == Product " + index;
                    msg += "\n== Name: " + invoice.Products[index].Name;
                    msg += "\n== Price: " + invoice.Products[index].Price;
                }

                MessageBox.Show(msg);
            }
            catch (Exception)
            {
                MessageBox.Show("Object couldn't be deserialized.\nCheck you input data.");
            }
        }
    }
}
