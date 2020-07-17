using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Microsoft.PowerShell.Commands;
using TextBackup.Backup;

namespace TextBackup.PS.Cmdlet
{
    [Cmdlet(VerbsData.Backup, "TextBackup")]
    public class BackupTextBackup : PSCmdlet
    {
        [Parameter(Mandatory = true), Alias("Path")]
        public string FilePath { get; set; }

        protected override void ProcessRecord()
        {
            BackupProcessResult ret = BackupWork.Backup(FilePath);
            WriteObject(ret);
        }
    }
}
