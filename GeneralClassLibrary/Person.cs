using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralClassLibrary
{
    public class Person
    {
        public string Name="";
        public string SurName="";
        public string Patronymic = "";
        public string Company = "";
        public string Email = "";
        public Bitmap QrCode=null;
        
        public Person() { }

        public Person(string name, string surname, string patronymic, string company, Bitmap qrcode) {
            Name= name;
            SurName= surname;
            Patronymic= patronymic;
            Company= company;
            QrCode= qrcode;
        }

        public string ToString()
        {
            return $"{SurName} {Name} {Patronymic} {Company}";
        }
    }
}
