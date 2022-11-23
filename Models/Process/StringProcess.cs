using System.Text.RegularExpressions;
namespace DoanQuocVietBTTH2.Models.Process
{
    public class StringProcess
    {
        public string AutoGenerateCode(string StrInput)
        {
        string strResult = "", numPart = "",strPart ="";
        //tach phan tu so strInput
        //VD strInput = "STD001 => numPart = "001"
        numPart = Regex.Match(StrInput,@"\d+").Value;
        //tách phần tử strInput
        //VD strInput = "STD001 => numPart = "STD"
        strPart = Regex .Match(StrInput,@"\D").Value;
        //tăng phần số lên 1 đơn vị
        int inPart = (Convert.ToInt32(numPart)+1);
        //bổ sung các ký tự 0 còn thiếu
        for(int i = 0; i <numPart.Length - inPart.ToString().Length;i++ )
        {
            strPart += "0";        
        }
        strResult = strPart + inPart;
        return strResult;

        }
    }
}