using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;


namespace TSFastBuild
{
    public class BeforeTsc : Task
    {
        [Required]
        public ITaskItem[] FullPathToFiles { get; set; }
        [Output]
        public bool TscRequired { get; set; }

        public override bool Execute()
        {
            this.TscRequired = this.IsTscRequired();
            return true;
        }

        private bool IsTscRequired()
        {
            bool compile = false;

            foreach (ITaskItem item in this.FullPathToFiles)
            {

                var tsFile = new System.IO.FileInfo(item.ItemSpec);
                var jsFile = new System.IO.FileInfo(tsFile.FullName.Substring(0, tsFile.FullName.Length - 2) + "js");


                if (!item.ItemSpec.EndsWith(".d.ts"))
                {

                    if (jsFile.Exists == false)
                    {
                        compile = true;
                        this.Log.LogMessage(MessageImportance.High, "TSC: Must compile typescript because js file does not exists: {0} ", tsFile.FullName);
                        break;
                    }
                    else if (jsFile.LastWriteTimeUtc < tsFile.LastWriteTimeUtc)
                    {
                        compile = true;
                        this.Log.LogMessage(MessageImportance.High, "TSC: Must compile typescript because js file is not up-to-date : {0} ", jsFile.FullName);
                        break;

                    }
                }
            }

            if (compile == false)
            {
                this.Log.LogMessage(MessageImportance.High, "TSC: All files are up-to-date.");
            }

            return compile;
        }
    }
}
