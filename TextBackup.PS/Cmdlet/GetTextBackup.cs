using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using TextBackup.Backup;

namespace TextBackup.PS.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "TextBackup")]
    public class GetTextBackup : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            List<BackupSummary> summary = BackupWork.Summary();
            WriteObject(summary);
        }
    }
}
