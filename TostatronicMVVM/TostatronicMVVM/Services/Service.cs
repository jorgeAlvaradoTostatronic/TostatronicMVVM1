using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TostatronicMVVM.Services
{
    class Service
    {
        

        public static string GetData(string url)
        {
            string json = "[{'codigo':'BCSCUU8','nombre':'SCSUU8 Para impresora 3D','existencia':'59','precio_publico':'45','precio_distribuidor':'40','precio_minimo':'35','imagen':'BCSCUU8.png'},{'codigo':'BRCARAB','nombre':'Case Raspberry Pi 3B','existencia':'0','precio_publico':'120','precio_distribuidor':'110','precio_minimo':'100','imagen':'BRCARAB.png'},{'codigo':'BRCARAB3BP','nombre':'Raspberry PI 3B+','existencia':'10','precio_publico':'1060','precio_distribuidor':'1045','precio_minimo':'1000','imagen':'BRCARAB3BP.png'},{'codigo':'BRFILPLAAZ','nombre':'Filamento PLA 1.75mm Azul','existencia':'0','precio_publico':'400','precio_distribuidor':'320','precio_minimo':'320','imagen':'BRFILPLAAZ.png'}]";
            return json;
            //string json = JsonConvert.SerializeObject("");
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";
            using (StreamWriter writes = new StreamWriter(request.GetRequestStream()))
            {
                writes.Write(json);
            }
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();
            string json1 = "";
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                    json1 += reader.ReadLine();
            }
            return json1;
        }
    }
}
