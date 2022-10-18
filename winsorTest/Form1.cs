using ExcelDataReader;
using MapWinGIS;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winsorTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        DataTable dataTable1 = new DataTable("Continent");
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //转换为点shp文件
        private void ConvertPoint(DataTable table, string saveFile, string xColName, string yColName)
        {
            long LastLineNumber = 0;
            try
            {
                var FieldIndices = new Hashtable();
                var FieldRenames = new Hashtable();

                MapWinGIS.ShpfileType sftype;
                sftype = MapWinGIS.ShpfileType.SHP_POINT;

                MapWinGIS.Shapefile newSF = new MapWinGIS.Shapefile();
                newSF.CreateNew(saveFile, sftype);
                newSF.StartEditingShapes(true);

                MapWinGIS.Field idFld = new MapWinGIS.Field();
                idFld.Key = "MWShapeID";
                idFld.name = "MWShapeID";
                idFld.Precision = 10;
                idFld.Type = MapWinGIS.FieldType.INTEGER_FIELD;
                int fldIdx = newSF.NumFields;
                newSF.EditInsertField(idFld, fldIdx);
                FieldIndices.Add("MWShapeID", fldIdx);

                foreach (DataRow Vals in table.Rows)
                {
                    LastLineNumber += 1;

                    Application.DoEvents();

                    //string[] Vals = line.Split(cmbDelim.Text.ToCharArray()[0]);

                    DataRow dataRow = Vals;

                    // ConditionValues(ref dataRow);

                    MapWinGIS.Shape newShp = new MapWinGIS.Shape();
                    newShp.Create(sftype);

                    MapWinGIS.Point newPt = new MapWinGIS.Point();
                    //获取
                    for (int i = 0; i <= table.Columns.Count - 1; i++)
                    {
                        string colName = table.Columns[i].ColumnName;
                        double coo = GetCoordinates(Vals[i].ToString());
                        if (coo == -1) return;
                        Vals[i] = coo;//将单元格值转化为数字格式
                        if (colName == xColName)
                        {
                            newPt.x = coo;
                        }
                        else if (colName == yColName)
                        {
                            newPt.y = coo;
                        }
                        //else if (colName == cmbM.Text && colName != "")
                        //{
                        //    newPt.Z = coo;
                        //}
                        //else if (colName == cmbZ.Text && colName != "")
                        //{
                        //    newPt.M = coo;
                        //}
                    }

                    newShp.InsertPoint(newPt, 0);
                    int shpIdx = newSF.NumShapes;
                    newSF.EditInsertShape(newShp, ref shpIdx);

                    if (newSF.LastErrorCode != 0 && newSF.ErrorMsg[newSF.LastErrorCode] != "No Error")
                        MessageBox.Show(newSF.ErrorMsg[newSF.LastErrorCode]);

                    //若表中包含filed对应列名
                    for (int i = 0; i <= table.Columns.Count - 1; i++)
                    {
                        string colName = table.Columns[i].ColumnName;
                        // Always edit the fields for the x, y, z, m
                        // values regardless of value of chkAttribs 
                        if (colName != "" && FieldRenames.Contains(i))
                            newSF.EditCellValue((int)FieldRenames[i], shpIdx, Vals[i]);
                        else if (colName != "" && FieldIndices.Contains(colName))
                            newSF.EditCellValue((int)FieldIndices[colName], shpIdx, Vals[i]);
                    }

                    //  newShp = null/* TODO Change to default(_) if this is not a reference type */;
                    // newPt = null/* TODO Change to default(_) if this is not a reference type */;
                    // GC.Collect();

                }
                // tr.Close();

                newSF.StopEditingShapes(true, true);
                newSF.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //坐标由字符串转化为数字
        private double GetCoordinates(string coordinates)
        {
            double coo = 0;
            if (double.TryParse(coordinates, out coo))
            {
                return coo;
            }

            char[] array = coordinates.ToCharArray();
            var byteArray = Encoding.ASCII.GetBytes(array);
            List<char> list = new List<char>();
            for (int i = 0; i < byteArray.Length; i++)
            {
                int item = byteArray[i];
                if (item < 48 || item > 57)
                {
                    list.Add(array[i]);
                }
            }

            var cooArray = coordinates.Split(list.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            double temp = 0;
            //换算
            for (int i = 0; i < cooArray.Length; i++)
            {
                if (!double.TryParse(cooArray[i], out temp))
                {
                    MessageBox.Show("存在格式不正确的数据", "提示");
                    return -1;
                }

                if (i == 0)
                {
                    coo += temp;
                }
                else if (i == 1)
                {
                    coo += temp / 60.0;
                }
                else if (i == 2)
                {
                    coo += temp / 3600.0;
                }
            }
            // return Math.Round(coo, 4);
            return coo;
        }
        //将csv文件读取为datatable
        private DataTable GetTableFromFile(string fileName)
        {
            //1：打开文件，得到文件stream
            var streamData = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //2：得到文件reader（需要NuGet包ExcelDataReader）

            var readerData = ExcelReaderFactory.CreateOpenXmlReader(streamData);
            //3：通过reader得到数据（需要NuGet包ExcelDataReader.DataSet ）
            ExcelDataReader.ExcelDataSetConfiguration setConfig = new ExcelDataSetConfiguration();
            var result = readerData.AsDataSet();
            //4：得到ExcelFile文件的表Sheet
            var dataTable = result.Tables[0];

            for (int i = 0; i <= dataTable.Columns.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(dataTable.Columns[i].ToString()))
                    dataTable.Columns[i].ColumnName = dataTable.Columns[i].ToString();
            }

            return dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable inputData = GetTableFromFile("");
            ConvertPoint(inputData, "", "", "");
        }

      
    }
}
