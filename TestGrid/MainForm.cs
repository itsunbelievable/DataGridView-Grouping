using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DgvFilterPopup;
using OutlookStyleControls;

namespace TestGrid
{
    public partial class MainForm : Form
    {
        DataGridViewGrouper dgv;
        public MainForm()
        {
            InitializeComponent();
            dgv = new DataGridViewGrouper(dataGridView1);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            dgv.CollapseAll();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            nameTA.Fill(this.dbDataSet.name);
            DgvFilterManager filterManager = new DgvFilterManager(dataGridView1);
            dgv.SetGroupOn("phone");

            outlookGrid1.BindData(dbDataSet,"name");

            outlookGrid1.GroupTemplate = new OutlookGridAlphabeticGroup();
            outlookGrid1.GroupTemplate.Column = phoneDataGridViewTextBoxColumn;
            outlookGrid1.GroupTemplate.Collapsed = true;
            //used custom datarowcomparer
            outlookGrid1.Sort(new DataRowComparer(4,ListSortDirection.Ascending));
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            dgv.ExpandAll();
        }

        public class DataRowComparer : IComparer
        {
            ListSortDirection direction;
            int columnIndex;

            public DataRowComparer(int columnIndex, ListSortDirection direction)
            {
                this.columnIndex = columnIndex;
                this.direction = direction;
            }

            #region IComparer Members

            public int Compare(object x, object y)
            {

                DataRow obj1 = (DataRow)x;
                DataRow obj2 = (DataRow)y;
                return string.Compare(obj1[columnIndex].ToString(), obj2[columnIndex].ToString()) * (direction == ListSortDirection.Ascending ? 1 : -1);
            }
            #endregion
        }
    }
}
