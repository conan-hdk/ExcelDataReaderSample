using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration.Install;

using System.Windows.Forms;

namespace CustomSetup
{
	[System.ComponentModel.RunInstaller(true)]
    public class CustomInstaller:Installer
    {
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			base.Install(stateSaver);
			var r = MessageBox.Show("セットアップを継続しますか？","Install",MessageBoxButtons.YesNo);
			if (r != DialogResult.Yes)
			{
				throw new InstallException("Installエラー!");
			}
		}
		public override void Commit(System.Collections.IDictionary savedState)
		{
			base.Commit(savedState);
			MessageBox.Show("Commit!");
		}
		public override void Rollback(System.Collections.IDictionary savedState)
		{
			base.Rollback(savedState);
			MessageBox.Show("Rollback!");
		}
		public override void Uninstall(System.Collections.IDictionary savedState)
		{
			base.Uninstall(savedState);
			MessageBox.Show("Uninstall!");
		}
    }
}
