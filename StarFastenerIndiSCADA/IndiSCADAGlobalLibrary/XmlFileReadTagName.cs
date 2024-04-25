using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IndiSCADAGlobalLibrary
{
    public static class XmlFileReadTagName
    {
        private static bool CheckTaglist(string[] TaglistVal)
        {
            int count = 0;
            for (int i = 0; i < TaglistVal.Length; i++)
            {
                if (TaglistVal[i] == "" || TaglistVal[i] == null)
                {
                    count++;
                }

            }
            if (count > 0)
                return false;
            else
                return true;
        }
        public static string ReadTagName(string tagname, int position)
        {
            String[] Tagvalue = new string[1];
            string[] addresslist = new string[] { };
            try
            {
                DataSet dsaddresslist = new DataSet();
                dsaddresslist.ReadXml("EIP.xml");
                DataRow[] dr = dsaddresslist.Tables[0].Select("Name='" + tagname + "'");

                if (dr[0].ItemArray[2].ToString() == "Random")
                {

                    if (dr.Count() != 0)
                    {
                        ushort strSize1 = Convert.ToUInt16(dr[0].ItemArray[5]);
                        string taglist = dr[0].ItemArray[4].ToString();

                        string[] TaglistArray = taglist.Split(',');

                        bool validlist = CheckTaglist(TaglistArray);

                        if (validlist)
                        {

                            Tagvalue = TaglistArray;

                            return Tagvalue[position];
                        }
                        else
                        { return null; }


                    }
                    return null;
                }
                else if (dr[0].ItemArray[2].ToString() == "Block")
                {
                    string taglist = dr[0].ItemArray[4].ToString();
                    int strSize2 = Convert.ToInt32(dr[0].ItemArray[5]);

                    int[] arradd = new int[strSize2];

                    int errorCounter = Regex.Matches(taglist, @"[a-zA-Z]").Count;

                    if (errorCounter == 0)// if address not contain any character
                    {
                        int address = Convert.ToInt16(taglist);

                        for (int index = 0; index <= strSize2 - 1; index++)
                        {

                            arradd[index] = address + index;

                            Tagvalue = Array.ConvertAll(arradd, x => x.ToString());

                        }

                        return Tagvalue[position];
                    }
                    else// if address contain character character 
                    {
                        string splitstr1 = taglist[0].ToString();
                        string splitstr2 = taglist.Remove(0, 1).ToString();
                        int addresslistarr = Convert.ToInt16(splitstr2);
                        int[] arradd1 = new int[strSize2];

                        for (int index = 0; index <= strSize2 - 1; index++)
                        {

                            arradd1[index] = addresslistarr + index;

                            Tagvalue = Array.ConvertAll(arradd1, x => splitstr1 + x.ToString());
                        }

                        return Tagvalue[position];
                    }


                }

                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
