using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;

namespace xmldoc_dom1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            string xmlFilename = Application.StartupPath + @"\invoices.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilename);

            XmlElement root = doc.DocumentElement;

            XmlNodeList allInvoices = root.GetElementsByTagName("invoice");
            foreach (XmlNode anInvoice in allInvoices)
            {
                // Read patient Name.
                XmlNode patient = anInvoice.FirstChild;
                sb.AppendLine();//blank line.
                sb.AppendLine("Name: " + patient.Attributes["firstname"].Value);
                sb.AppendLine("SSN: " + patient.Attributes["SSN"].Value);

                sb.AppendLine("Procedures and Cost:");
                // Extract procedures for this invoice
                // starting at 1 because zero was <patient>
                for (int i = 1; i < anInvoice.ChildNodes.Count; i++)
                {
                    if (anInvoice.ChildNodes[i].Name.Equals("procedure"))
                    {
                        sb.AppendLine(anInvoice.ChildNodes[i].Attributes["name"].Value + 
                            ", "+ anInvoice.ChildNodes[i].Attributes["cost"].Value);
                    }
                }
            }// for each invoice ends.

            txbInfo.Text = sb.ToString();

        }
    }
}
