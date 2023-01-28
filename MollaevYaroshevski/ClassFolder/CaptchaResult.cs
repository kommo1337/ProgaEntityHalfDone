using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MollaevYaroshevski.ClassFolder
{
    public class CaptcaResult
    {
        public string CaptchaCode { get; set; }
        public byte[] CaptchaByteData { get; set; }
        public string CaptchaBase64 => Convert.ToBase64String(CaptchaByteData);
        public DateTime TimeStamp { get; set; }
    }
}
