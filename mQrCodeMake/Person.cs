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
        public string Email = "";
        public string Company = "";
        public List<string> Events= new List<string>();        
        public List<bool>  IsOchn = new List<bool>();        
        public Bitmap QrCode=null;

        public string Fio
        {
            set
            {
                string[] temp = value.Split();
                Name = temp[0];
                SurName= temp[1];
                Patronymic = temp[2];
            }
        }

        public Person() { }

        public Person(string name, string surname, string patronymic, string company, Bitmap qrcode) {
            Name= name;
            SurName= surname;
            Patronymic= patronymic;
            Company= company;
            QrCode= qrcode;
        }

        public override string ToString()
        {
            return $"{SurName} {Name} {Patronymic} {Company}";
        }
    }
}
