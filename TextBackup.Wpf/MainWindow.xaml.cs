using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using TextBackup.Backup;
using System.IO;

namespace TextBackup.Wpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<BackupSummaryForTreeView> backupSummaries =
            new ObservableCollection<BackupSummaryForTreeView>();

        private Uri icon_Backup = new Uri(System.IO.Path.GetFullPath(@"Image\BackupIcon_gray.png"));
        private Uri icon_Restore = new Uri(System.IO.Path.GetFullPath(@"Image\ResotreIcon_gray.png"));
        private Uri icon_Remove = new Uri(System.IO.Path.GetFullPath(@"Image\RemoveIcon_gray.png"));

        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<BackupSummaryForTreeView> tempSummaries =
                new ObservableCollection<BackupSummaryForTreeView>();
            BackupWork.SummaryForTreeViewinListView(tempSummaries);
            backupSummaries = new ObservableCollection<BackupSummaryForTreeView>(
                tempSummaries.OrderBy(x => x.Summary[0].Name));

            listView.ItemsSource = backupSummaries;
        }

        /// <summary>
        /// Backupボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backupButton_Click(object sender, RoutedEventArgs e)
        {
            TreeView tView = (TreeView)((StackPanel)((Button)sender).Parent).Children[0];
            BackupSummary bkSummary =
                (tView.Items.SourceCollection as ObservableCollection<BackupSummary>)[0];
            BackupWork.Backup(bkSummary.FilePath);

            BackupWork.UpdateSummary(backupSummaries, bkSummary.FilePath);

            //  ここでバックアップ完了の通知を表示
        }

        /// <summary>
        /// Restoreボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            TreeView tView = (TreeView)((StackPanel)((Button)sender).Parent).Children[0];
            BackupSummary.GenerationSummary genSummary =
                tView.SelectedValue as BackupSummary.GenerationSummary;
            BackupSummary bkSummary =
                (tView.Items.SourceCollection as ObservableCollection<BackupSummary>)[0];
            if (genSummary != null && bkSummary != null)
            {
                BackupWork.Restore(bkSummary.FilePath, genSummary.Index);
            }
        }

        /// <summary>
        /// Removeボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            TreeView tView = (TreeView)((StackPanel)((Button)sender).Parent).Children[0];
            BackupSummary.GenerationSummary genSummary =
                tView.SelectedValue as BackupSummary.GenerationSummary;
            BackupSummary bkSummary =
                (tView.Items.SourceCollection as ObservableCollection<BackupSummary>)[0];
            if (genSummary != null && bkSummary != null)
            {
                int index = genSummary.Index;
                if (index == bkSummary.Generations.Count - 1)
                {
                    BackupWork.RemoveNewest(bkSummary.FilePath);
                }
                else
                {
                    BackupWork.Remove(bkSummary.FilePath, genSummary.Index);
                }
                BackupWork.UpdateSummary(backupSummaries, bkSummary.FilePath);
            }
        }

        /// <summary>
        /// TreeView内の選択変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //  バックアップ対象ファイルサマリ
            ObservableCollection<BackupSummary> tree =
                ((TreeView)sender).Items.SourceCollection as ObservableCollection<BackupSummary>;
            textBK.Text = string.Format(
                "Name: {0}\r\n" +
                "FilePath: {1}",
                tree[0].Name, tree[0].FilePath);

            //  バックアップ対象の世代サマリ
            var gen = ((TreeView)sender).SelectedValue as BackupSummary.GenerationSummary;
            if (gen == null)
            {
                textGen.Text = "";
            }
            else
            {
                textGen.Text = string.Format(
                    "Name: {0}\r\n" +
                    "Description: {1}\r\n" +
                    "Status: {2}\r\n" +
                    "Date: {3}\r\n" +
                    "DiffWeight: {4}\r\n",
                    gen.Name, gen.Description, gen.Status, gen.Date, gen.DiffWeight);
            }
        }

        private void imageIcon_Initialized(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            switch (image.Name)
            {
                case "image_backupIcon":
                    ((Image)sender).Source = new BitmapImage(icon_Backup);
                    break;
                case "image_restoreIcon":
                    ((Image)sender).Source = new BitmapImage(icon_Restore);
                    break;
                case "image_removeIcon":
                    ((Image)sender).Source = new BitmapImage(icon_Remove);
                    break;
            }
        }
    }
}
