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

namespace flexserializer_demo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serializeCustomer = new System.Windows.Forms.Button();
            this.serializePreferredCustomer = new System.Windows.Forms.Button();
            this.serializeInvoice = new System.Windows.Forms.Button();
            this.deserializeInvoice = new System.Windows.Forms.Button();
            this.deserializePreferredCustomer = new System.Windows.Forms.Button();
            this.deserializeCustomer = new System.Windows.Forms.Button();
            this.objectData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // serializeCustomer
            // 
            this.serializeCustomer.Location = new System.Drawing.Point(13, 13);
            this.serializeCustomer.Name = "serializeCustomer";
            this.serializeCustomer.Size = new System.Drawing.Size(169, 23);
            this.serializeCustomer.TabIndex = 0;
            this.serializeCustomer.Text = "Serialize Customer";
            this.serializeCustomer.UseVisualStyleBackColor = true;
            this.serializeCustomer.Click += new System.EventHandler(this.serializeCustomer_Click);
            // 
            // serializePreferredCustomer
            // 
            this.serializePreferredCustomer.Location = new System.Drawing.Point(13, 43);
            this.serializePreferredCustomer.Name = "serializePreferredCustomer";
            this.serializePreferredCustomer.Size = new System.Drawing.Size(169, 23);
            this.serializePreferredCustomer.TabIndex = 1;
            this.serializePreferredCustomer.Text = "Serialize PreferredCustomer";
            this.serializePreferredCustomer.UseVisualStyleBackColor = true;
            this.serializePreferredCustomer.Click += new System.EventHandler(this.serializePreferredCustomer_Click);
            // 
            // serializeInvoice
            // 
            this.serializeInvoice.Location = new System.Drawing.Point(13, 73);
            this.serializeInvoice.Name = "serializeInvoice";
            this.serializeInvoice.Size = new System.Drawing.Size(169, 23);
            this.serializeInvoice.TabIndex = 2;
            this.serializeInvoice.Text = "Serialize Invoice";
            this.serializeInvoice.UseVisualStyleBackColor = true;
            this.serializeInvoice.Click += new System.EventHandler(this.serializeInvoice_Click);
            // 
            // deserializeInvoice
            // 
            this.deserializeInvoice.Location = new System.Drawing.Point(188, 73);
            this.deserializeInvoice.Name = "deserializeInvoice";
            this.deserializeInvoice.Size = new System.Drawing.Size(169, 23);
            this.deserializeInvoice.TabIndex = 5;
            this.deserializeInvoice.Text = "Deserialize Invoice";
            this.deserializeInvoice.UseVisualStyleBackColor = true;
            this.deserializeInvoice.Click += new System.EventHandler(this.deserializeInvoice_Click);
            // 
            // deserializePreferredCustomer
            // 
            this.deserializePreferredCustomer.Location = new System.Drawing.Point(188, 43);
            this.deserializePreferredCustomer.Name = "deserializePreferredCustomer";
            this.deserializePreferredCustomer.Size = new System.Drawing.Size(169, 23);
            this.deserializePreferredCustomer.TabIndex = 4;
            this.deserializePreferredCustomer.Text = "Deserialize PreferredCustomer";
            this.deserializePreferredCustomer.UseVisualStyleBackColor = true;
            this.deserializePreferredCustomer.Click += new System.EventHandler(this.deserializePreferredCustomer_Click);
            // 
            // deserializeCustomer
            // 
            this.deserializeCustomer.Location = new System.Drawing.Point(188, 13);
            this.deserializeCustomer.Name = "deserializeCustomer";
            this.deserializeCustomer.Size = new System.Drawing.Size(169, 23);
            this.deserializeCustomer.TabIndex = 3;
            this.deserializeCustomer.Text = "Deserialize Customer";
            this.deserializeCustomer.UseVisualStyleBackColor = true;
            this.deserializeCustomer.Click += new System.EventHandler(this.deserializeCustomer_Click);
            // 
            // objectData
            // 
            this.objectData.Location = new System.Drawing.Point(13, 103);
            this.objectData.Multiline = true;
            this.objectData.Name = "objectData";
            this.objectData.Size = new System.Drawing.Size(528, 147);
            this.objectData.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 262);
            this.Controls.Add(this.objectData);
            this.Controls.Add(this.deserializeInvoice);
            this.Controls.Add(this.deserializePreferredCustomer);
            this.Controls.Add(this.deserializeCustomer);
            this.Controls.Add(this.serializeInvoice);
            this.Controls.Add(this.serializePreferredCustomer);
            this.Controls.Add(this.serializeCustomer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Flex Serializer Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button serializeCustomer;
        private System.Windows.Forms.Button serializePreferredCustomer;
        private System.Windows.Forms.Button serializeInvoice;
        private System.Windows.Forms.Button deserializeInvoice;
        private System.Windows.Forms.Button deserializePreferredCustomer;
        private System.Windows.Forms.Button deserializeCustomer;
        private System.Windows.Forms.TextBox objectData;
    }
}

