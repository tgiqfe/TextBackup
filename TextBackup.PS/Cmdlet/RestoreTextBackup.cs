using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using TextBackup.Backup;

namespace TextBackup.PS.Cmdlet
{
    [Cmdlet(VerbsData.Restore, "TextBackup")]
    public class RestoreTextBackup : PSCmdlet
    {
        [Parameter(Mandatory = true), Alias("Path")]
        public string FilePath { get; set; }
        [Parameter]
        public int? Index { get; set; }

        protected override void ProcessRecord()
        {
            BackupProcessResult result = BackupWork.Restore(FilePath, (int)Index);
            WriteObject(result);
        }
    }
}
