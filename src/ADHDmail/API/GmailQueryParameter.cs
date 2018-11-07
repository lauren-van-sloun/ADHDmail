using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    enum GmailQueryParameter
    {
        From,
        To,
        Subject,
        Label,
        HasAttachment,
        HasFilename,
        Contains,
        AllFolders,
        Starred,
        Unread,
        Read,
        After,
        Before,
        DeliveredTo,
        LargerThan,
        SmallerThan,
        MatchesWordExactly
    }
}
