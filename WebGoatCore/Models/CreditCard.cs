using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using WebGoatCore.Exceptions;

namespace WebGoatCore.Models
{
    public class CreditCard
    {
        #region Public properties
        /// <summary>The XML file in which credit card numbers are stored.</summary>
        public string Filename { get; set; }
        public string Username { get; set; }
        public string Number { get; set; }
        public DateTime Expiry { get; set; }
        public int ExpiryMonth => Expiry.Month;
        public int ExpiryYear => Expiry.Year;
        #endregion

        #region Constructor
        public CreditCard()
        {
            Filename = string.Empty;
            Username = string.Empty;
            Number = string.Empty;
        }
        #endregion

        #region Public methods
        public void GetCardForUser()
        {
            XDocument document = ReadCreditCardFile();
            try
            {
                XElement ccElement = GetCreditCardXmlElement(document);

                Username = Username;
                var expiryElement = ccElement.Element("Expiry");
                if (expiryElement != null)
                {
                    Expiry = Convert.ToDateTime(expiryElement.Value);
                }

                var numberElement = ccElement.Element("Number");
                if (numberElement != null)
                {
                    Expiry = Convert.ToDateTime(numberElement.Value);
                }
            }
            catch (IndexOutOfRangeException)     //File exists but has nothing in it.
            {
                CreateNewCreditCardFile();
            }
            catch (XmlException)     //File is corrupt. Delete and recreate.
            {
                CreateNewCreditCardFile();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private XElement GetCreditCardXmlElement(XDocument document)
        {
            var ccElement = document.Descendants("CreditCard").FirstOrDefault(c =>
            {
                var userNameElement = c.Element("Username");

                return userNameElement != null && userNameElement.Value.Equals(Username);
            });
            if (ccElement != null && ccElement.HasElements)
            {
                return ccElement;
            }
            throw new WebGoatCreditCardNotFoundException(string.Format("No card found for {0}.", Username));
        }

        public void SaveCardForUser()
        {
            if (CardExistsForUser())
            {
                UpdateCardForUser();
            }
            else
            {
                InsertCardForUser();
            }
        }

        /// <summary>Validates the card</summary>
        /// <returns>True if valid.  False otherwise.</returns>
        /// <remarks>
        /// This only validates according to Luhn's algorithm and date.  In the real world, we'd also
        /// query the credit card company.
        /// </remarks>
        public bool IsValid()
        {
            if (Expiry < DateTime.Today || string.IsNullOrEmpty(this.Number))
            {
                return false;
            }

            // Remove non-digits
            var creditCardNumber = Regex.Replace(Number, @"[^\d]", "");

            // minimal valid number length = 13
            if (string.IsNullOrEmpty(creditCardNumber) || creditCardNumber.Length < 13)
            {
                return false;
            }

            var number = creditCardNumber.ToCharArray();

            // Validate based on card type, first if tests length, second tests prefix
            // Use Luhn Algorithm to validate
            int sum = 0;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                if (i % 2 == number.Length % 2)
                {
                    int n = (number[i] - '0') * 2;
                    sum += (n / 10) + (n % 10);
                }
                else
                {
                    sum += (number[i] - '0');
                }
            }
            return (sum % 10 == 0);
        }

        public string ChargeCard(decimal amount)
        {
            //Here is where we'd actually charge the card if this were real.
            var code = new Random().Next(999999).ToString("000000");
            return code;
        }
        #endregion

        #region Private methods
        private XDocument ReadCreditCardFile()
        {
            var document = new XDocument();
            try
            {
                using (FileStream readStream = File.Open(Filename, FileMode.Open))
                {
                    var xr = new XmlTextReader(readStream);
                    document = XDocument.Load(xr);
                    xr.Close();
                }
            }
            catch (FileNotFoundException)    //File does not exist - Create it.
            {
                CreateNewCreditCardFile();
            }
            catch (XmlException)     //File is corrupt. Delete and recreate.
            {
                CreateNewCreditCardFile();
            }
            catch (Exception)
            {
                throw;
            }
            return document;
        }

        private void WriteCreditCardFile(XDocument xmlDocument)
        {
            try
            {
                using (var writeStream = File.Open(Filename, FileMode.Create))
                {
                    var writer = new StreamWriter(writeStream);
                    xmlDocument.Save(writer);
                    writer.Close();
                }
            }
            catch (FileNotFoundException)    //File does not exist - Create it.
            {
                CreateNewCreditCardFile();
            }
            catch (IndexOutOfRangeException)     //File exists but has nothing in in.
            {
                CreateNewCreditCardFile();
            }
            catch (XmlException)     //File is corrupt. Delete and recreate.
            {
                CreateNewCreditCardFile();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CardExistsForUser()
        {
            var document = ReadCreditCardFile();
            try
            {
                GetCreditCardXmlElement(document);
            }
            catch (WebGoatCreditCardNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        private void CreateNewCreditCardFile()
        {
            var document = new XDocument();
            document.Add(new XElement("CreditCards"));
            WriteCreditCardFile(document);
        }

        private void UpdateCardForUser()
        {
            XDocument document = ReadCreditCardFile();
            try
            {
                GetCardForUser();
                WriteCreditCardFile(document);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InsertCardForUser()
        {
            XDocument document = ReadCreditCardFile();
            try
            {
                var root = document.Root;
                if(root != null)
                {
                    root.Add(new XElement("CreditCard",
                        new XElement("Username", Username),
                        new XElement("Number", Number),
                        new XElement("Expiry", Expiry)
                        ));
                    WriteCreditCardFile(document);
                }
                else
                {
                    // this should never happen
                    throw new WebGoatFatalException("Cannot access credit card storage!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
