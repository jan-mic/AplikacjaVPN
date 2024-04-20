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
            textBox.Text = textBox.Text.Trim();
            textBox.Select(textBox.Text.Length, 0);
        }

        public void RemoveLastChar(TextBox textBox)
        {
            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            textBox.Select(textBox.Text.Length, 0);
        }

        public bool IsDigit(string text)
        {
            //Regex regex = new Regex("[^0-9]+");
            Regex regex = new Regex("[0-9]");
            return !regex.IsMatch(text);
        }

        public bool IsLetter(string text)
        {
            //Regex regex = new Regex("[^a-zA-z]+");
            Regex regex = new Regex("[a-zA-z]");
            return !regex.IsMatch(text);
        }

        


        /*//////////////////////

        public bool IsLetterOrDigit(Key key)
        {
            return !IsLetter(key) && !IsNumber(key);
        }
        public bool IsNumber(Key key)
        {
            if (key < Key.D0 || key > Key.D9)
            {
                if (key < Key.NumPad0 || key > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsLetter(Key key)
        {
            if (key < Key.A || key > Key.Z)
            {
                return false;
            }
            return true;
        }

        public bool IsLetter2(Key key)
        {
            //Char.IsLetterOrDigit(key.Char);
            if (key < Key.A || key > Key.Z)
            {
                return false;
            }
            return true;
        }*/
    }
}
