using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.Readers
{
    public interface Reader
    {
        void ReadChar(int index, string str);
        bool IsActive { get; set; }
    }
}
