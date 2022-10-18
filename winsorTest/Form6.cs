using Castle.Core.Internal;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winsorTest
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        List<string> Fields = new List<string>();
        Hashtable FieldIndices; // 'Field indices by field name
        Hashtable FieldRenames;// 'Field number from CSV to SF field index

        private void Button1_Click(System.Object sender, System.EventArgs e)
        {
            FieldIndices = new Hashtable();
           
            OpenFileDialog fod = new OpenFileDialog();
            fod.Filter = "Text Files (*.txt, *.csv)|*.txt;*.csv";
            if (fod.ShowDialog() ==DialogResult.OK)
                txtInput.Text = fod.FileName;
        }
        DataTable  sheet;
        private void Button2_Click(System.Object sender, System.EventArgs e)
        {
            //if (cmbDelim.Text == "")
            //{
            //    GroupBox1.Enabled = false;
            //    GroupBox2.Enabled = false;
            //   MessageBox.Show ("Please select a field delimiter.",  "Select a Delimiter");
            //    return;
            //}
            try
            {
                if (!System.IO.File.Exists(txtInput.Text))
                {
                    GroupBox1.Enabled = false;
                    GroupBox2.Enabled = false;
                   MessageBox.Show ("The specified file doesn't exist! Please click the Browse button to locate a file.",  "No Such File");
                    return;
                }
                else
                {
                    //1：打开文件，得到文件stream
                    var streamData = File.Open(txtInput.Text, FileMode.Open, FileAccess.Read);
                    //2：得到文件reader（需要NuGet包ExcelDataReader）
                   
                    var readerData = ExcelReaderFactory.CreateOpenXmlReader(streamData);
                    //3：通过reader得到数据（需要NuGet包ExcelDataReader.DataSet ）
                    ExcelDataReader.ExcelDataSetConfiguration setConfig = new ExcelDataSetConfiguration();
                    var result = readerData.AsDataSet();
                    //4：得到ExcelFile文件的表Sheet
                     sheet = result.Tables[0];
                    
                    streamData.Close();
                    
                    cmbX.Items.Clear();
                    cmbY.Items.Clear();
                    cmbZ.Items.Clear();
                    cmbM.Items.Clear();
                    cmbShapeID.Items.Clear();
                    cmbPartID.Items.Clear();
                    cmbPartID.Items.Add("N/A (Not Needed)");
                    Fields.Clear();

                    for (int i = 0; i <= sheet.Columns.Count- 1; i++)
                    {

                        var ColumnCaption = sheet.Rows[0][i].ToString();
                        Fields.Add(ColumnCaption);
                       
                        if (!string.IsNullOrEmpty(ColumnCaption))
                        {
                            cmbX.Items.Add(ColumnCaption);
                            cmbY.Items.Add(ColumnCaption);
                            cmbZ.Items.Add(ColumnCaption);
                            cmbM.Items.Add(ColumnCaption);
                            cmbShapeID.Items.Add(ColumnCaption);
                            cmbPartID.Items.Add(ColumnCaption);
                        }
                       
                    }

                    for (int i = 0; i <= sheet.Columns.Count - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(Fields[i]))
                            sheet.Columns[i].ColumnName = Fields[i];
                    }

                    sheet.Rows.RemoveAt(0);

                    for (int i = 0; i <= cmbX.Items.Count - 1; i++)
                    {
                        if (cmbX.Items[i].ToString().ToLower() == "x")
                            cmbX.SelectedIndex = i;
                    }
                    for (int i = 0; i <= cmbY.Items.Count - 1; i++)
                    {
                        if (cmbY.Items[i].ToString().ToLower() == "y")
                            cmbY.SelectedIndex = i;
                    }
                    for (int i = 0; i <= cmbZ.Items.Count - 1; i++)
                    {
                        if (cmbZ.Items[i].ToString().ToLower() == "z")
                            cmbZ.SelectedIndex = i;
                    }
                    for (int i = 0; i <= cmbM.Items.Count - 1; i++)
                    {
                        if (cmbM.Items[i].ToString().ToLower() == "m")
                            cmbM.SelectedIndex = i;
                    }
                    if (cmbX.SelectedIndex == -1 & cmbX.Items.Count > 0)
                        cmbX.SelectedIndex = 0;
                    if (cmbY.SelectedIndex == -1 & cmbY.Items.Count > 0)
                        cmbY.SelectedIndex = 0;

                    GroupBox1.Enabled = true;
                    GroupBox2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show (ex.Message , "Error Occurred");
            }
        }

        private void btnConvert_Click(System.Object sender, System.EventArgs e)
        {
            if (cmbX.Text == "" | cmbY.Text == "")
            {
                MessageBox.Show("Please select at least an X and Y column.", "Please Select X and Y Column");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Shapefiles (*.shp)|*.shp";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            if (System.IO.File.Exists(sfd.FileName))
            {
                System.IO.File.Delete(sfd.FileName);
                System.IO.File.Delete(System.IO.Path.ChangeExtension(sfd.FileName, ".shx"));
                System.IO.File.Delete(System.IO.Path.ChangeExtension(sfd.FileName, ".dbf"));
            }

            if (rdPoint.Checked)
                ConvertPoint(sfd.FileName);
            else if (rdPolygon.Checked)
                ConvertPolygonLine(0, sfd.FileName);
            else if (rdLine.Checked)
                ConvertPolygonLine(1, sfd.FileName);
        }

        private void ConditionValues(ref string[] vals)
        {
            // Check to make sure that something didn't get split by a comma when it
            // shouldn't have... e.g., "Reston" and " VA" should have been "Reston, VA"
            // First Pass:
            ArrayList UnmatchedStarting = new ArrayList();
            ArrayList UnmatchedEnding = new ArrayList();
            for (int i = 0; i <= vals.Length - 2; i++)
            {
                if (vals[i].StartsWith("") & !vals[i].EndsWith(""))
                    UnmatchedStarting.Add(i);
                else if (vals[i].EndsWith("") & !vals[i].StartsWith(""))
                    UnmatchedEnding.Add(i);
            }

            if (UnmatchedStarting.Count != UnmatchedEnding.Count)
                ConditionValues_OneCommaOnly(ref vals); // Fallback - has some disadvantages
            else
            {
                ArrayList newVals = new ArrayList();
                string Append = "";
                bool Appending = false;
                for (int i = 0; i <= vals.Length - 1; i++)
                {
                    if (!UnmatchedStarting.Contains(i) & !UnmatchedEnding.Contains(i) & !Appending)
                        newVals.Add(vals[i]);
                    else if ((int)UnmatchedEnding[0] == i)
                    {
                        UnmatchedStarting.RemoveAt(0);
                        UnmatchedEnding.RemoveAt(0);
                        Append += vals[i];
                        newVals.Add(Append);
                        Append = "";
                        Appending = false;
                    }
                    else
                    {
                        Appending = true;
                        Append += vals[i] + ",";
                    }
                }
               
                 vals = Array.ConvertAll<object, string>(newVals.ToArray(),z=>Convert.ToString(z));  
            }

            for (int i = 0; i <= vals.Length - 1; i++)
                vals[i] = vals[i].Trim();

            for (int i = 0; i <= vals.Length - 1; i++)
            {
                if (vals[i].StartsWith("'") && vals[i].EndsWith("'"))
                    vals[i] = vals[i].Substring(1, vals[i].Length - 2);
            }
        }

        private void ConditionValues_OneCommaOnly(ref string[] vals)
        {
            // Check to make sure that something didn't get split by a comma when it
            // shouldn't have... e.g., "Reston" and " VA" should have been "Reston, VA"
            for (int i = 0; i <= vals.Length - 1; i++)
                vals[i] = vals[i].Trim();

            int endCond = vals.Length - 2;
            for (int i = 0; i <= endCond; i++)
            {
                // If endcond changes, .NET seems to still cache the old endCond.
                // Helpful.
                if (i > endCond)
                    break;

                if (vals[i].StartsWith("") & !vals[i].EndsWith("") & !vals[i + 1].StartsWith("") & vals[i + 1].EndsWith(""))
                {
                    // We probably have a problem here...
                    string[] newVals = new string[vals.Length - 2 + 1];
                    for (int q = 0; q <= vals.Length - 2; q++)
                    {
                        // Reload, skipping item i and merging back in
                        if (q < i)
                            newVals[q] = vals[q];
                        else if (q == i)
                            newVals[q] = vals[q] + ", " + vals[q + 1];
                        else
                            newVals[q] = vals[q + 1];
                    }
                    // Update vals
                    vals = newVals;
                    endCond = vals.Length - 2;
                    // Reset i to rescan all
                    i = 0;
                }
            }

            for (int i = 0; i <= vals.Length - 1; i++)
            {
                if (vals[i].StartsWith("") & vals[i].EndsWith(""))
                    vals[i] = vals[i].Substring(1, vals[i].Length - 2);
                if (vals[i].StartsWith("'") & vals[i].EndsWith("'"))
                    vals[i] = vals[i].Substring(1, vals[i].Length - 2);
            }

        }

        private void ConvertPolygonLine(int plType, string filename)
        {
            long LastLineNumber = 0;
            try
            {
                FieldIndices = new Hashtable();
                FieldRenames = new Hashtable();

                MapWinGIS.ShpfileType sftype;

                if (plType == 0)
                {
                    sftype = MapWinGIS.ShpfileType.SHP_POLYGON;
                    if (cmbZ.Text != "")
                        sftype = MapWinGIS.ShpfileType.SHP_POLYGONZ;
                    if (cmbM.Text != "")
                        sftype = MapWinGIS.ShpfileType.SHP_POLYGONM;
                }
                else
                {
                    sftype = MapWinGIS.ShpfileType.SHP_POLYLINE;
                    if (cmbZ.Text != "")
                        sftype = MapWinGIS.ShpfileType.SHP_POLYLINEZ;
                    if (cmbM.Text != "")
                        sftype = MapWinGIS.ShpfileType.SHP_POLYLINEM;
                }

                MapWinGIS.Shapefile newSF = new MapWinGIS.Shapefile();
                newSF.CreateNew(filename, sftype);
                newSF.StartEditingShapes(true);

                MapWinGIS.Field idFld = new MapWinGIS.Field();
                idFld.Key = "MWShapeID";
                idFld.name = "MWShapeID";
                idFld.Precision = 10;
                idFld.Type = MapWinGIS.FieldType.INTEGER_FIELD;
                
                int fldIdx = newSF.NumFields;
                newSF.EditInsertField(idFld, fldIdx);
                FieldIndices.Add("MWShapeID", fldIdx);

                // ...the rest:
                for (int i = 0; i <= Fields.Count - 1; i++)
                {
                    // Always add the fields for the x, y, z, m
                    // iff chkCoordsIntoAttribs.Checked
                    // Add everything if chkAttribs.checked

                    // Prevent a duplicate field name
                    string newField = Fields[i];
                    int incr = 1;
                    if (FieldIndices.Contains(newField))
                    {
                        while (FieldIndices.Contains(newField.Substring(0, 8) + incr.ToString()))
                            incr += 1;
                        newField = newField.Substring(0, 8) + incr.ToString();
                    }

                    if (newField != "")
                    {
                        if (chkAttribs.Checked & newField != cmbX.Text & newField != cmbY.Text & newField != cmbM.Text & newField != cmbZ.Text)
                        {
                            MapWinGIS.Field newFld = new MapWinGIS.Field();
                            newFld.Key = newField.Replace(" ", "");
                            newFld.name = newField.Replace(" ", "");
                            newFld.Width = 50;
                            newFld.Type = MapWinGIS.FieldType.STRING_FIELD;
                            fldIdx = newSF.NumFields;
                            newSF.EditInsertField(newFld, fldIdx);
                            FieldIndices.Add(newField, fldIdx);
                            if (Fields[i] != newField)
                                FieldRenames.Add(i, fldIdx);
                        }
                        else if (chkCoordsIntoAttribs.Checked & (newField == cmbX.Text | newField == cmbY.Text | newField == cmbM.Text | newField == cmbZ.Text))
                        {
                            MapWinGIS.Field newFld = new MapWinGIS.Field();
                            newFld.Key = newField.Replace(" ", "");
                            newFld.name = newField.Replace(" ", "");
                            newFld.Width = 50;
                            newFld.Type = MapWinGIS.FieldType.STRING_FIELD;
                            fldIdx = newSF.NumFields;
                            newSF.EditInsertField(newFld, fldIdx);
                            FieldIndices.Add(newField, fldIdx);
                            if (Fields[i] != newField)
                                FieldRenames.Add(i, fldIdx);
                        }
                    }
                }

                DataRow  FirstLineVals = null;
                bool FirstRead = false;
               
                int LastShapeID = -1;
                int LastPartID = 0;
                int LastPointID = 0;
                MapWinGIS.Shape newShp = new MapWinGIS.Shape(); /* TODO Change to default(_) if this is not a reference type */;

                //while (tr.Peek() != -1)
                //{
                //    line = tr.ReadLine();
                foreach (DataRow Vals in sheet.Rows)
                {
                    LastLineNumber += 1;
                    lblPrg.Text = "Working on line " + LastLineNumber.ToString();
                    lblPrg.Refresh();
                    Application.DoEvents();

                    FirstRead = true;

                    for (int i = 0; i <= Fields.Count - 1; i++)
                    {
                        if (cmbPartID.SelectedIndex > 0)
                        {
                            if (Fields[i] == cmbPartID.Text)
                            {
                                int @int;
                                if (!int.TryParse(Vals[i].ToString(), out @int))
                                {
                                    MessageBox.Show("One or more of the part ID values isn't numeric! Please make sure to select the correct fields.", "Non-Numeric Value");
                                    return;
                                }
                                @int -= 1; // Zero-based
                                if (@int != LastPartID & newShp != null)
                                    if (@int != LastPartID & newShp != null)
                                    {
                                        LastPartID = @int;
                                        if (@int > 0)
                                            newShp.InsertPart(LastPointID, @int);
                                    }
                            }
                        }
                        else
                        {
                        }

                        if (Fields[i] == cmbShapeID.Text)
                        {
                            int @int;
                            if (!int.TryParse(Vals[i].ToString(), out @int))
                            {
                                MessageBox.Show("One or more of the shape ID values isn't numeric! Please make sure to select the correct fields.", "Non-Numeric Value");
                                return;
                            }
                            if (@int != LastShapeID)
                            {
                                LastShapeID = @int;
                                // Finish off the shape if there is one; otherwise, create new
                                if (newShp != null)
                                {
                                    int shpIdx = newSF.NumShapes;
                                    newSF.EditInsertShape(newShp, ref shpIdx);

                                    for (int j = 0; j <= Fields.Count - 1; j++)
                                    {
                                        // Always edit the fields for the x, y, z, m
                                        // values regardless of value of chkAttribs 
                                        //if (Fields[j] != "" && FieldRenames.Contains(j))
                                        //    newSF.EditCellValue((int)FieldRenames[j], shpIdx, FirstLineVals[j]);
                                        //else if (Fields[j] != "" && FieldIndices.Contains(Fields[j]) )
                                        //    newSF.EditCellValue((int)FieldIndices[Fields[j]], shpIdx, FirstLineVals[j]);
                                    }

                                    // The new "first line values" are those just read now:
                                    FirstLineVals = Vals;
                                }
                                newShp = new MapWinGIS.Shape();
                                newShp.Create(sftype);
                                newShp.InsertPart(0, 0);
                            }
                        }
                    }

                    MapWinGIS.Point newPt = new MapWinGIS.Point();

                    for (int i = 0; i <= Fields.Count - 1; i++)
                    {
                        double coo = GetCoordinates(Vals[i].ToString());
                        if (coo == -1) return;
                        Vals[i] = coo;
                        if (Fields[i] == cmbX.Text)
                        {
                            newPt.x = coo;
                        }
                        else if (Fields[i] == cmbY.Text)
                        {
                            newPt.y = coo;
                        }
                        else if (Fields[i] == cmbM.Text & Fields[i] != "")
                        {
                            newPt.Z = coo;
                        }
                        else if (Fields[i] == cmbZ.Text & Fields[i] != "")
                        {
                            newPt.M = coo;
                        }
                    }

                    newShp.InsertPoint(newPt, LastPointID);
                    LastPointID += 1;


                    // tr.Close();

                    // Finish any remaining shape if we need to
                    if (newShp != null)
                    {
                        int shpIdx = newSF.NumShapes;
                        newSF.EditInsertShape(newShp, shpIdx);

                        for (int j = 0; j <= Fields.Count - 1; j++)
                        {
                            // Always edit the fields for the x, y, z, m
                            // values regardless of value of chkAttribs 
                            if (Fields[j] != "" && FieldRenames.Contains(j))
                                newSF.EditCellValue((int)FieldRenames[j], shpIdx, Vals[j]);
                            else if (Fields[j] != "" && FieldIndices.Contains(Fields[j]))
                                newSF.EditCellValue((int)FieldIndices[Fields[j]], shpIdx, Vals[j]);
                        }
                    }
                }

                newSF.StopEditingShapes(true, true);
                newSF.Close();

                lblPrg.Text = "Complete!";

                MessageBox.Show ("The file has been converted.",  "Complete");
            }
            catch (Exception ex)
            {
                lblPrg.Text = "Error Occurred";
                MessageBox.Show("An error occurred while trying to read the file (on or around line " + LastLineNumber.ToString() + "). Potential problems include:" + "Is the delimiter correct?" +  "Are there carriage returns in a field value?" +  "Are commas used instead of periods for decimal places?" +  "Is the number of fields equal throughout the file?" +  "If problems persist, please post your CSV file at http://bugs.mapwindow.org.", "Error Occurred");
            }
        }

        private void ConvertPoint(string filename)
        {
            long LastLineNumber = 0;
            try
            {
                FieldIndices = new Hashtable();
                FieldRenames = new Hashtable();

                MapWinGIS.ShpfileType sftype;
                sftype = MapWinGIS.ShpfileType.SHP_POINT;
                if (cmbZ.Text != "")
                    sftype = MapWinGIS.ShpfileType.SHP_POINTZ;
                if (cmbM.Text != "")
                    sftype = MapWinGIS.ShpfileType.SHP_POINTM;

                MapWinGIS.Shapefile newSF = new MapWinGIS.Shapefile();
                newSF.CreateNew(filename, sftype);
                newSF.StartEditingShapes(true);
                
                MapWinGIS.Field idFld = new MapWinGIS.Field();
                idFld.Key = "MWShapeID";
                idFld.name = "MWShapeID";
                idFld.Precision = 10;
                idFld.Type = MapWinGIS.FieldType.INTEGER_FIELD;
                int fldIdx = newSF.NumFields;
                newSF.EditInsertField(idFld, fldIdx);
                FieldIndices.Add("MWShapeID", fldIdx);

                // ...the rest:
                for (int i = 0; i <= Fields.Count  - 1; i++)
                {
                    string newField = Fields[i];
                    int incr = 1;
                    if (FieldIndices.Contains(newField))
                    {
                        while (FieldIndices.Contains(newField.Substring(0, 8) + incr.ToString()))
                            incr += 1;
                        newField = newField.Substring(0, 8) + incr.ToString();
                    }

                    if (newField != "")
                    {
                        if (chkAttribs.Checked & newField != cmbX.Text & newField != cmbY.Text & newField != cmbM.Text & newField != cmbZ.Text)
                        {
                            MapWinGIS.Field newFld = new MapWinGIS.Field();
                            newFld.Key = newField.Replace(" ", "");
                            newFld.name = newField.Replace(" ", "");
                            newFld.Width = 50;
                            newFld.Type = MapWinGIS.FieldType.STRING_FIELD;
                            fldIdx = newSF.NumFields;
                            newSF.EditInsertField(newFld, fldIdx);
                            FieldIndices.Add(newField, fldIdx);
                            if (Fields[i] != newField)
                                FieldRenames.Add(i, fldIdx);
                            newFld = null/* TODO Change to default(_) if this is not a reference type */;
                        }
                        else if (chkCoordsIntoAttribs.Checked & (newField == cmbX.Text | newField == cmbY.Text | newField == cmbM.Text | newField == cmbZ.Text))
                        {
                            MapWinGIS.Field newFld = new MapWinGIS.Field();
                            newFld.Key = newField.Replace(" ", "");
                            newFld.name = newField.Replace(" ", "");
                            newFld.Width = 50;
                            newFld.Type = MapWinGIS.FieldType.STRING_FIELD;
                            fldIdx = newSF.NumFields;
                            newSF.EditInsertField(newFld, fldIdx);
                            FieldIndices.Add(newField, fldIdx);
                            if (Fields[i] != newField)
                                FieldRenames.Add(i, fldIdx);
                            newFld = null/* TODO Change to default(_) if this is not a reference type */;
                        }
                    }
                }

                //while (tr.Peek() != -1)
                //{
                //    line = tr.ReadLine();

                foreach (DataRow Vals in sheet.Rows)
                {
                    LastLineNumber += 1;
                    lblPrg.Text = "Working on line " + LastLineNumber.ToString();
                    lblPrg.Refresh();
                    Application.DoEvents();

                    //string[] Vals = line.Split(cmbDelim.Text.ToCharArray()[0]);

                    DataRow dataRow = Vals;

                    // ConditionValues(ref dataRow);

                    MapWinGIS.Shape newShp = new MapWinGIS.Shape();
                    newShp.Create(sftype);

                    MapWinGIS.Point newPt = new MapWinGIS.Point();

                    for (int i = 0; i <= Fields.Count - 1; i++)
                    {
                        double coo = GetCoordinates(Vals[i].ToString());
                        if (coo == -1) return;
                        Vals[i] = coo;
                        if (Fields[i] == cmbX.Text)
                        {
                            newPt.x = coo;
                        }
                        else if (Fields[i] == cmbY.Text)
                        {
                            newPt.y = coo;
                        }
                        else if (Fields[i] == cmbM.Text & Fields[i] != "")
                        {
                            newPt.Z = coo;
                        }
                        else if (Fields[i] == cmbZ.Text & Fields[i] != "")
                        {
                            newPt.M = coo;
                        }
                    }

                    newShp.InsertPoint(newPt, 0);
                    int shpIdx = newSF.NumShapes;
                    newSF.EditInsertShape(newShp, ref shpIdx);

                    if (newSF.LastErrorCode != 0 && newSF.ErrorMsg[newSF.LastErrorCode] != "No Error")
                        MessageBox.Show(newSF.ErrorMsg[newSF.LastErrorCode]);

                    for (int i = 0; i <= Fields.Count - 1; i++)
                    {
                        // Always edit the fields for the x, y, z, m
                        // values regardless of value of chkAttribs 
                        if (Fields[i] != "" && FieldRenames.Contains(i))
                            newSF.EditCellValue((int)FieldRenames[i], shpIdx, Vals[i]);
                        else if (Fields[i] != "" && FieldIndices.Contains(Fields[i]))
                            newSF.EditCellValue((int)FieldIndices[Fields[i]], shpIdx, Vals[i]);
                        
                    }

                    //  newShp = null/* TODO Change to default(_) if this is not a reference type */;
                    // newPt = null/* TODO Change to default(_) if this is not a reference type */;
                    // GC.Collect();

                }
               // tr.Close();

                newSF.StopEditingShapes(true, true);
                newSF.Close();

                lblPrg.Text = "Complete!";

                // Interaction.MsgBox("The file has been converted.", MsgBoxStyle.Information, "Complete");
            }
            catch (Exception ex)
            {
                lblPrg.Text = "Error Occurred";
                // Interaction.MsgBox("An error occurred while trying to read the file (on or around line " + LastLineNumber.ToString() + "). Potential problems include:" + Constants.vbCrLf + Constants.vbCrLf + "Is the delimiter correct?" + Constants.vbCrLf + "Are there carriage returns in a field value?" + Constants.vbCrLf + "Are commas used instead of periods for decimal places?" + Constants.vbCrLf + "Is the number of fields equal throughout the file?" + Constants.vbCrLf + Constants.vbCrLf + "If problems persist, please post your CSV file at http://bugs.mapwindow.org.", MsgBoxStyle.Exclamation, "Error Occurred");
            }
        }

        //坐标由字符串转化为数字
        private double GetCoordinates(string coordinates)
        {
            double coo = 0;
            if (double.TryParse(coordinates,out coo))
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
           
            var cooArray = coordinates.Split(list.ToArray(),StringSplitOptions.RemoveEmptyEntries  );

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

        private void rdType_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (rdPoint.Checked)
            {
                cmbShapeID.Enabled = false;
                cmbPartID.Enabled = false;
                cmbShapeID.Text = "(for Lines/Polygons Only)";
                cmbPartID.Text = "(for Lines/Polygons Only)";
            }
            else
            {
                cmbShapeID.Enabled = true;
                cmbPartID.Enabled = true;
                cmbPartID.SelectedIndex = 0;
                if (cmbShapeID.Items.Count > 0)
                    cmbShapeID.SelectedIndex = 0;
            }
        }


        #region 坐标转换
        void GuaussProj_6(double latd_d, double lontd_d, double Guss6_x, double Guss6_y) //高斯坐标6度带正算

        {
            double a = 6378137;  //椭球长轴，1975年参数 没有用现在元数据中给的a=6378245
            const double e1_2 = 0.0066943799; //第一偏心率e1的平方
            double e2_2 = 0.0067394967; //第二偏心率e2的平方 
            double z = 0.0174532925; // pi/180
            double ro = 57.2957795;  // 180/pi
                                     //const double r = 206265;
            double p = 1e-10;
            double A = 1.0050517739; //A,B,C,D为沿经线弧长公式的系数
            double B = 0.00506237764;
            double C = 0.0000106245;
            double D = 0.00000002081;

            int zone;         //带号
            int center_lontd; //中央经线
            double X, Y;      //高斯坐标
            double N;         //卯酉圈半径
            double eta_2;     //eta的平方，计算时需用到的参数
            double s;         //沿经线方向的弧长
                              //double lontd_d;   //角度表示的经度
                              //double latd_d;    //角度表示的纬度

            double latd_r;    //弧度表示的纬度
            double lontd_cha;     //所给经度与中央经线的经差
            double lontd_2, lontd_3, lontd_4, lontd_5; //经差的三、四、五次方
            double sin_latd, cos_latd, tan_latd, cos_latd_3, cos_latd_5, tan_latd_4;  //纬度的三角函数值

            latd_r = latd_d / ro;//纬度弧度制
                                 // latd_r = latd_d;//纬度弧度制
                                 // latd_d =latd_d*ro  ;//纬度角度制
                                 // lontd_d =lontd_d*ro ;//经度角度值



            if (lontd_d / 6 != 0)  //求带号
                zone = (int)(lontd_d / 6) + 1;
            else
                zone = (int)(lontd_d / 6);

            center_lontd = 6 * zone - 3; //求中央经线
            lontd_cha = lontd_d - center_lontd; //求经差
            lontd_cha = lontd_cha * z;  //经差转换为弧度

            lontd_2 = lontd_cha * lontd_cha;
            lontd_3 = lontd_cha * lontd_cha * lontd_cha;
            lontd_4 = lontd_cha * lontd_cha * lontd_cha * lontd_cha;
            lontd_5 = lontd_cha * lontd_cha * lontd_cha * lontd_cha * lontd_cha;
            sin_latd =Math.Sin  (latd_r);
            cos_latd = Math.Cos(latd_r);
            tan_latd = Math.Tan(latd_r);
            cos_latd_3 = cos_latd * cos_latd * cos_latd;  //三次方
            cos_latd_5 = cos_latd * cos_latd * cos_latd * cos_latd * cos_latd;  //五次方
            tan_latd_4 = tan_latd * tan_latd * tan_latd * tan_latd;

            N = a / Math.Sqrt((1 - e1_2 * sin_latd * sin_latd));  //计算卯酉圈半径
            eta_2 = e2_2 * cos_latd * cos_latd;
            s = a * (1 - e1_2) * ((A / ro) * latd_d - (B / 2) * Math.Sin(2 * latd_r) + (C / 4) * Math.Sin(4 * latd_r) - (D / 6) * Math.Sin(6 * latd_r));

            X = s + (lontd_2 * N / 2) * sin_latd * cos_latd + (lontd_4 * N / 24) * sin_latd * cos_latd_3 * (5 - tan_latd * tan_latd + 9 * eta_2 + 4 * eta_2 * eta_2);
            Y = lontd_cha * N * cos_latd + lontd_3 * N * cos_latd_3 * (1 - tan_latd * tan_latd + eta_2) / 6 + lontd_5 * N * cos_latd_5 * (5 - 18 * tan_latd * tan_latd + tan_latd_4);

            Y = Y + 500000;
            double temp_Y = zone * 1000000;
            //m_X = X;
            //m_Y = temp_Y + Y;  //在Y坐标前加上带号
            Guss6_x = X;
            Guss6_y = Y;

        }

        /*----------------
        '函数名称：Trf_XYtoBL
        '功能:高斯克吕格坐标系统反算
        '接口：[IN]x,y，高斯坐标 (按值传递)
        '[IN]L0,中央子午线,CorD 高斯坐标带类型(3度/6度)
        '[OUT]b,l 站点大地坐标值 (按地址传递)

        */
        void xytobl(double x, double y, double B , double L , double L0, double CorD)
        {

            double R = 6367558.496863;//'地球椭球半径
            double e2 = 0.00673852415;//'第二偏心率值

            double G2, H2, I2, J2, k2, L2, N2, M2
              , O2, P2, Q2, R2, S2, T2, U2, V2
             , W2, X2, ABSY
              , tmp1, tmp2, tmp3,
             nDebtNo;

            G2 = x / R;
            H2 = Math.Cos(G2);
            I2 = Math.Sin(G2);
            J2 = I2 * I2;
            tmp1 = J2 * 2.38209 * Math.Pow(10, -7);
            tmp2 = J2 * (2.983718 * Math.Pow(10, -5));
            tmp3 = I2 * H2 * (5.051773759 * Math.Pow(10, -3));
            k2 = G2 + tmp3 - tmp2 - tmp1;
            L2 = Math.Cos(k2);
            M2 = e2 * L2 * L2;
            N2 = 1 + M2;
            O2 = 6399698.9018 / Math.Sqrt(N2);
            P2 = Math.Tan(k2);
            Q2 = P2 * P2;
            //nDebtNo = IIf(CorD = 6, ((L0 + 3) / 6), IIf(CorD = 3, (L0 / 3), 0))
            // nDebtNo = (CorD = 6, (L0 + 3) / 6) ? ((L0 + 3) / 6) : (CorD = 3) ? (L0 / 3) : 0;
            nDebtNo = 0;
             R2 = nDebtNo * Math.Pow(10, 6) + 5 * Math.Pow(10, 5);
            
            if (y > 500000 && y < nDebtNo * Math.Pow(10, 6))
                y = y - 500000 * 2;

            ABSY = Math.Abs(y);
            S2 = (ABSY < 500000) ? (ABSY - 500000) / O2 : (ABSY - R2) / O2;
            T2 = S2 * S2;
            B = (k2 - ((((45 * Q2 + 90) * Q2 + 61) * T2 / 30 - (3 - 9 * M2) * Q2
                 - 5 - M2) * T2 / 12 + 1) * T2 * P2 * N2 / 2) * 180 / (4 * Math.Atan(1.000001));
            L = ((((24 * Q2 + 28) * Q2 + (8 * Q2 + 6) * M2 + 5) * T2 / 20
                 - 2 * Q2 - N2) * T2 / 6 + 1) * S2 / L2 * 180 / (4 * Math.Atan(1.00000001));


            if (y < 0) L = L0 - L;
            else L = L0 + L;

        }
        #endregion

        private void Form6_Load(object sender, EventArgs e)
        {
            GetCoordinates("108.2.3");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}


