using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoConverter.Model
{
    public class InputVideo
    {
        public string VideoUrl { get; set; }
        public string TargetQuality { get; set; }
        public string TargetSize { get; set; }
    }
}
