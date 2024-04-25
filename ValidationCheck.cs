using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace VpnApplication
{
    internal class ValidationCheck
    {
        public bool IsLetterOrDigit(string text)
        {
            return IsLetter(text) && IsDigit(text);
        }
        public void RemoveSpaces(TextBox textBox)
        {
            
            if (textBox.Text.Any(Char.IsWhiteSpace))
            {
                textBox.Text= Regex.Replace(textBox.Text, @"\s", "");
                textBox.Select(textBox.Text.Length, 0);
            }
        }

        public void RemoveLastChar(TextBox textBox)
        {
            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            textBox.Select(textBox.Text.Length, 0);
        }

        public bool IsDigit(string text)
        {
            Regex regex = new Regex("[0-9]");
            return !regex.IsMatch(text);
        }

        public bool IsLetter(string text)
        {
            Regex regex = new Regex("[a-zA-z]");
            return !regex.IsMatch(text);
        }

        public void CheckFirstDigit(TextBox textBox)
        {
            if (textBox.Text.Length > 0)
            {
                if (textBox.Text[0] == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                }
            }
        }

        public void CheckFirstDigitForInterface(TextBox textBox)
        {
            if (textBox.Text.Length > 0)
            {
                string[] parts = Regex.Split(textBox.Text, @"/");


                for (int i = 0; i < parts.Length; i++)
                {
                    if(parts[i].Length > 1)
                    {
                        if (parts[i][0] == '0')
                        {
                            parts[i] = parts[i].Remove(0, 1);
                        }
                    }

                    
                }

                textBox.Text = string.Join("/", parts);
            }
        }

        public void CheckIP(TextBox textBox)
        {
            if (textBox.Text.Length > 1)
            {
                if (textBox.Text[0] == '0')
                {
                    textBox.Text = textBox.Text.Remove(0, 1);
                }
            }
        }
    }
}
