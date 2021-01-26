using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Captcha
    {
        private static bool Is_Number(string text)
        {

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '0' || text[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }
        public static bool Captcha_Validation(string captcha_text)
        {
            if (captcha_text.Length < 5)
            {
                return false;

            }
            else
            {
                string temp = "";
                for (int i = 0; i < 5; i++)
                {
                    temp += captcha_text[i];
                }
                return Is_Number(temp);
            }
        }
    }
}
