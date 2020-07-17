using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using TextBackup.Backup;

namespace TextBackup.PS.Cmdlet
{
    [Cmdlet(VerbsCommon.Remove, "TextBackup")]
    public class RemoveTextBackup : PSCmdlet
    {
        [Parameter(Mandatory = true), Alias("Path")]
        public string FilePath { get; set; }
        [Parameter]
        public int? Index { get; set; }
        [Parameter]
        public SwitchParameter Oldest { get; set; }
        [Parameter]
        public SwitchParameter Newest { get; set; }

        protected override void ProcessRecord()
        {
            if (Newest)
            {
                BackupProcessResult newestRet = BackupWork.RemoveNewest(FilePath);
                WriteObject(newestRet);
            }
            else if (Oldest)
            {
                BackupProcessResult oldestRet = BackupWork.RemoveOldest(FilePath);
                WriteObject(oldestRet);
            }
            else if (Index != null)
            {
                BackupProcessResult ret = BackupWork.Remove(FilePath, (int)Index);
                WriteObject(ret);
            }
        }
    }
}
