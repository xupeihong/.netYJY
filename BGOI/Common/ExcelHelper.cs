using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using NPOI.HSSF;
using NPOI.SS.Util;
using NPOI.HSSF.Util;

namespace TECOCITY_BGOI
{
    public class ExcelHelper
    {
        /// <summary>
        /// 大标题1
        /// </summary>
        /// <param name="hSSFWorkbook">HSSFWorkbook</param>
        /// <returns>HSSFFont</returns>
        public static IFont FontTitle1(HSSFWorkbook hSSFWorkbook)
        {
            IFont mHSSFFont = hSSFWorkbook.CreateFont();
            mHSSFFont.FontName = "微软雅黑"; //字体类型
            mHSSFFont.FontHeightInPoints = 18;   //字号

            return mHSSFFont;
        }
        public static MemoryStream ExportDataTableToExcelH(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                IRow header2Row = sheet.CreateRow(1);
                IRow header3Row = sheet.CreateRow(2);
                IRow header4Row = sheet.CreateRow(3);

                headerRow.Height = 700;
                header2Row.Height = 350;
                header3Row.Height = 350;
                header4Row.Height = 350;

                sheet.SetColumnWidth(0, 10 * 256);
                sheet.SetColumnWidth(1, 10 * 256);
                sheet.SetColumnWidth(2, 25 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 25 * 256);
                sheet.SetColumnWidth(5, 25 * 256);
                sheet.SetColumnWidth(6, 15 * 256);
                sheet.SetColumnWidth(7, 15 * 256);
                sheet.SetColumnWidth(8, 20 * 256);
                sheet.SetColumnWidth(9, 20 * 256);

                ICell Icell = headerRow.CreateCell(0);
                Icell.CellStyle = SetCellStyle(workbook);
                Icell.SetCellValue("仪器设备（标准物质）配置一览表");
                ICell Icell1 = headerRow.CreateCell(1);
                Icell1.CellStyle = SetCellStyle(workbook);
                Icell1.SetCellValue("");
                ICell Icell2 = headerRow.CreateCell(2);
                Icell2.CellStyle = SetCellStyle(workbook);
                Icell2.SetCellValue("");
                ICell Icell3 = headerRow.CreateCell(3);
                Icell3.CellStyle = SetCellStyle(workbook);
                Icell3.SetCellValue("");
                ICell Icell4 = headerRow.CreateCell(4);
                Icell4.CellStyle = SetCellStyle(workbook);
                Icell4.SetCellValue("");
                ICell Icell5 = headerRow.CreateCell(5);
                Icell5.CellStyle = SetCellStyle(workbook);
                Icell5.SetCellValue("");
                ICell Icell6 = headerRow.CreateCell(6);
                Icell6.CellStyle = SetCellStyle(workbook);
                Icell6.SetCellValue("");
                ICell Icell7 = headerRow.CreateCell(7);
                Icell7.CellStyle = SetCellStyle(workbook);
                Icell7.SetCellValue("");
                ICell Icell8 = headerRow.CreateCell(8);
                Icell8.CellStyle = SetCellStyle(workbook);
                Icell8.SetCellValue("");
                ICell Icell9 = headerRow.CreateCell(9);
                Icell9.CellStyle = SetCellStyle(workbook);
                Icell9.SetCellValue("");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 9));

                ICell I2cell = header2Row.CreateCell(0);
                I2cell.CellStyle = SetCellStyle1(workbook);
                I2cell.SetCellValue("实验室详细地址：北京市朝阳区安外外馆东后街35号");
                ICell I2cell1 = header2Row.CreateCell(1);
                I2cell1.CellStyle = SetCellStyle1(workbook);
                I2cell1.SetCellValue("");
                ICell I2cell2 = header2Row.CreateCell(2);
                I2cell2.CellStyle = SetCellStyle1(workbook);
                I2cell2.SetCellValue("");
                ICell I2cell3 = header2Row.CreateCell(3);
                I2cell3.CellStyle = SetCellStyle1(workbook);
                I2cell3.SetCellValue("");
                ICell I2cell4 = header2Row.CreateCell(4);
                I2cell4.CellStyle = SetCellStyle1(workbook);
                I2cell4.SetCellValue("");
                ICell I2cell5 = header2Row.CreateCell(5);
                I2cell5.CellStyle = SetCellStyle1(workbook);
                I2cell5.SetCellValue("");
                ICell I2cell6 = header2Row.CreateCell(6);
                I2cell6.CellStyle = SetCellStyle1(workbook);
                I2cell6.SetCellValue("");
                ICell I2cell7 = header2Row.CreateCell(7);
                I2cell7.CellStyle = SetCellStyle1(workbook);
                I2cell7.SetCellValue("");
                ICell I2cell8 = header2Row.CreateCell(8);
                I2cell8.CellStyle = SetCellStyle1(workbook);
                I2cell8.SetCellValue("");
                ICell I2cell9 = header2Row.CreateCell(9);
                I2cell9.CellStyle = SetCellStyle1(workbook);
                I2cell9.SetCellValue("");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, 9));

                ICell I3cell = header3Row.CreateCell(0);
                I3cell.CellStyle = SetCellStyle2(workbook);
                I3cell.SetCellValue("序号");
                ICell I3cell1 = header3Row.CreateCell(1);
                I3cell1.CellStyle = SetCellStyle2(workbook);
                I3cell1.SetCellValue("仪器设备（标准物质）");
                ICell I3cell2 = header3Row.CreateCell(2);
                I3cell2.CellStyle = SetCellStyle2(workbook);
                I3cell2.SetCellValue("");
                ICell I3cell3 = header3Row.CreateCell(3);
                I3cell3.CellStyle = SetCellStyle2(workbook);
                I3cell3.SetCellValue("");
                ICell I3cell4 = header3Row.CreateCell(4);
                I3cell4.CellStyle = SetCellStyle2(workbook);
                I3cell4.SetCellValue("技术指标");
                ICell I3cell5 = header3Row.CreateCell(5);
                I3cell5.CellStyle = SetCellStyle2(workbook);
                I3cell5.SetCellValue("");
                ICell I3cell6 = header3Row.CreateCell(6);
                I3cell6.CellStyle = SetCellStyle2(workbook);
                I3cell6.SetCellValue("溯源方式");
                ICell I3cell7 = header3Row.CreateCell(7);
                I3cell7.CellStyle = SetCellStyle2(workbook);
                I3cell7.SetCellValue("有效截止日期至");
                ICell I3cell8 = header3Row.CreateCell(8);
                I3cell8.CellStyle = SetCellStyle2(workbook);
                I3cell8.SetCellValue("检定/校准单位名称");
                ICell I3cell9 = header3Row.CreateCell(9);
                I3cell9.CellStyle = SetCellStyle2(workbook);
                I3cell9.SetCellValue("备注");

                ICell I4cell = header4Row.CreateCell(0);
                I4cell.CellStyle = SetCellStyle2(workbook);
                I4cell.SetCellValue("");
                ICell I4cell1 = header4Row.CreateCell(1);
                I4cell1.CellStyle = SetCellStyle2(workbook);
                I4cell1.SetCellValue("编号");
                ICell I4cell2 = header4Row.CreateCell(2);
                I4cell2.CellStyle = SetCellStyle2(workbook);
                I4cell2.SetCellValue("名 称");
                ICell I4cell3 = header4Row.CreateCell(3);
                I4cell3.CellStyle = SetCellStyle2(workbook);
                I4cell3.SetCellValue("型号/规格");
                ICell I4cell4 = header4Row.CreateCell(4);
                I4cell4.CellStyle = SetCellStyle2(workbook);
                I4cell4.SetCellValue("测量范围");
                ICell I4cell5 = header4Row.CreateCell(5);
                I4cell5.CellStyle = SetCellStyle2(workbook);
                I4cell5.SetCellValue("准确度等级/ 不确定度 ");
                ICell I4cell6 = header4Row.CreateCell(6);
                I4cell6.CellStyle = SetCellStyle2(workbook);
                I4cell6.SetCellValue("");
                ICell I4cell7 = header4Row.CreateCell(7);
                I4cell7.CellStyle = SetCellStyle2(workbook);
                I4cell7.SetCellValue("");
                ICell I4cell8 = header4Row.CreateCell(8);
                I4cell8.CellStyle = SetCellStyle2(workbook);
                I4cell8.SetCellValue("");
                ICell I4cell9 = header4Row.CreateCell(9);
                I4cell9.CellStyle = SetCellStyle2(workbook);
                I4cell9.SetCellValue("");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 0, 0));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 1, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 6, 6));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 7, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 8, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 9, 9));
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色
                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色
                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色
                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充



                //    //set date format  
                //ICellStyle cellStyleDate = workbook.CreateCellStyle();
                //IDataFormat format = workbook.CreateDataFormat();
                //cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");

                //IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行

                //ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列


                //cellTitle.SetCellValue(a_strTitle);

                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, a_strCols.Length - 1));

                //cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                //cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //使用NPOI操作Excel表  
                //NPOI.SS.UserModel.IRow row = sheet.CreateRow(1);
                int count = 0;
                //for (int i = 0; i < a_strCols.Length; i++)//生成sheet第一行列名  
                //{
                //    sheet.SetColumnWidth(i, Convert.ToInt32(a_strCols[i].Split('-')[1]));
                //    NPOI.SS.UserModel.ICell cell = row.CreateCell(count++);
                //    cell.SetCellValue(a_strCols[i].Split('-')[0]);
                //    //cell.CellStyle = CellStyleContent(workbook);
                //    cell.CellStyle = mHSSFCellStyle;
                //}
                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 4);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }
        public static HSSFCellStyle SetCellStyle(HSSFWorkbook workbook)
        {
            HSSFCellStyle CellStyle = (HSSFCellStyle)workbook.CreateCellStyle();

            CellStyle.FillForegroundColor = HSSFColor.WHITE.index;
            CellStyle.WrapText = true;
            CellStyle.Alignment = HorizontalAlignment.CENTER;
            CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
            // fCellStyle.FontIndex = HSSFFont.FONT_ARIAL.;
            HSSFFont ffont = (HSSFFont)workbook.CreateFont();
            ffont.FontHeight = 15 * 15;
            ffont.FontName = "宋体";
            ffont.Boldweight = (short)FontBoldWeight.BOLD;
            //CellStyle.BorderBottom = BorderStyle.THIN;

            //CellStyle.BorderLeft = BorderStyle.THIN;
            //CellStyle.BorderRight = BorderStyle.THIN;
            //CellStyle.BorderTop = BorderStyle.THIN;
            // fCellStyle .BorderBottom
            // ffont.Color = HSSFColor.;
            CellStyle.SetFont(ffont);

            CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;//.Center;//垂直对齐
            CellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            return CellStyle;
        }
        public static HSSFCellStyle SetCellStyle1(HSSFWorkbook workbook)
        {
            HSSFCellStyle CellStyle = (HSSFCellStyle)workbook.CreateCellStyle();

            CellStyle.FillForegroundColor = HSSFColor.WHITE.index;
            CellStyle.WrapText = true;
            CellStyle.Alignment = HorizontalAlignment.LEFT;
            CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
            // fCellStyle.FontIndex = HSSFFont.FONT_ARIAL.;
            HSSFFont ffont = (HSSFFont)workbook.CreateFont();
            ffont.FontHeight = 15 * 15;
            ffont.FontName = "宋体";
            ffont.Boldweight = (short)FontBoldWeight.BOLD;
            //CellStyle.BorderBottom = BorderStyle.THIN;

            //CellStyle.BorderLeft = BorderStyle.THIN;
            //CellStyle.BorderRight = BorderStyle.THIN;
            //CellStyle.BorderTop = BorderStyle.THIN;
            // fCellStyle .BorderBottom
            // ffont.Color = HSSFColor.;
            CellStyle.SetFont(ffont);

            CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;//.Center;//垂直对齐
            CellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            return CellStyle;
        }
        public static HSSFCellStyle SetCellStyle2(HSSFWorkbook workbook)
        {
            HSSFCellStyle CellStyle = (HSSFCellStyle)workbook.CreateCellStyle();

            CellStyle.FillForegroundColor = HSSFColor.WHITE.index;
            CellStyle.WrapText = true;
            CellStyle.Alignment = HorizontalAlignment.CENTER;
            CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
            // fCellStyle.FontIndex = HSSFFont.FONT_ARIAL.;
            HSSFFont ffont = (HSSFFont)workbook.CreateFont();
            ffont.FontHeight = 15 * 15;
            ffont.FontName = "宋体";
            ffont.Boldweight = (short)FontBoldWeight.BOLD;
            CellStyle.BorderBottom = BorderStyle.THIN;

            CellStyle.BorderLeft = BorderStyle.THIN;
            CellStyle.BorderRight = BorderStyle.THIN;
            CellStyle.BorderTop = BorderStyle.THIN;
            // fCellStyle .BorderBottom
            // ffont.Color = HSSFColor.;
            CellStyle.SetFont(ffont);

            CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;//.Center;//垂直对齐
            CellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            return CellStyle;
        }



        public static ICellStyle CellStyleContent(HSSFWorkbook hSSFWorkbook)
        {
            ICellStyle mHSSFCellStyle = hSSFWorkbook.CreateCellStyle();
            mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
            //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
            mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框

            mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框

            mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框

            mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

            //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色

            //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色

            //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色

            //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

            mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
            //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
            //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
            //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充
            return mHSSFCellStyle;
        }


        /// <summary> 
        /// 将DataSet数据集转换HSSFworkbook对象，并保存为Stream流  
        /// </summary>  
        /// <param name="ds"></param>  
        /// <returns>返回数据流Stream对象</returns>  
        public static MemoryStream ExportDatasetToExcel(DataSet ds)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                //set date format  
                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
                int count = 0;
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)//生成sheet第一行列名  
                {
                    NPOI.SS.UserModel.ICell cell = row.CreateCell(count++);
                    cell.SetCellValue(ds.Tables[0].Columns[i].Caption);
                }
                //将数据导入到excel表中  
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 1);
                    count = 0;
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = ds.Tables[0].Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)ds.Tables[0].Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)ds.Tables[0].Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)ds.Tables[0].Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)ds.Tables[0].Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(ds.Tables[0].Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch
            {
                return new MemoryStream();
            }
        }

        /// <summary> 
        /// 将DataSet数据集转换HSSFworkbook对象，并保存为Stream流  
        /// </summary>  
        /// <param name="ds"></param>  
        /// <returns>返回数据流Stream对象</returns>  
        public static MemoryStream ExportDataTableToExcel(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");

                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框

                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框

                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框

                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色

                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色

                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色

                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充





                //set date format  
                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");

                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行

                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列

                cellTitle.SetCellValue(a_strTitle);

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, a_strCols.Length - 1));

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(1);
                int count = 0;
                for (int i = 0; i < a_strCols.Length; i++)//生成sheet第一行列名  
                {
                    sheet.SetColumnWidth(i, Convert.ToInt32(a_strCols[i].Split('-')[1]));
                    NPOI.SS.UserModel.ICell cell = row.CreateCell(count++);
                    cell.SetCellValue(a_strCols[i].Split('-')[0]);
                    //cell.CellStyle = CellStyleContent(workbook);
                    cell.CellStyle = mHSSFCellStyle;
                }
                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 2);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }


        public static MemoryStream ExportDataTableToExcelHs(DataTable dt, DataTable dts, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                IRow header2Row = sheet.CreateRow(1);
                IRow header3Row = sheet.CreateRow(2);
                IRow header4Row = sheet.CreateRow(3);
                IRow header5Row = sheet.CreateRow(4);
                headerRow.Height = 700;
                header2Row.Height = 350;
                header3Row.Height = 350;
                header4Row.Height = 350;
                header5Row.Height = 350;
                sheet.SetColumnWidth(0, 25 * 256);
                sheet.SetColumnWidth(1, 25 * 256);
                sheet.SetColumnWidth(2, 10 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 25 * 256);
                sheet.SetColumnWidth(5, 25 * 256);
                sheet.SetColumnWidth(6, 15 * 256);


                ICell Icell = headerRow.CreateCell(0);
                Icell.CellStyle = SetCellStyle(workbook);
                Icell.SetCellValue("零件采购单");
                ICell Icell1 = headerRow.CreateCell(1);
                Icell1.CellStyle = SetCellStyle(workbook);
                Icell1.SetCellValue("");
                ICell Icell2 = headerRow.CreateCell(2);
                Icell2.CellStyle = SetCellStyle(workbook);
                Icell2.SetCellValue("");
                ICell Icell3 = headerRow.CreateCell(3);
                Icell3.CellStyle = SetCellStyle(workbook);
                Icell3.SetCellValue("");
                ICell Icell4 = headerRow.CreateCell(4);
                Icell4.CellStyle = SetCellStyle(workbook);
                Icell4.SetCellValue("");
                ICell Icell5 = headerRow.CreateCell(5);
                Icell5.CellStyle = SetCellStyle(workbook);
                Icell5.SetCellValue("");
                ICell Icell6 = headerRow.CreateCell(6);
                Icell6.CellStyle = SetCellStyle(workbook);
                Icell6.SetCellValue("");

                ICell Icell7 = headerRow.CreateCell(7);
                Icell7.CellStyle = SetCellStyle(workbook);
                Icell7.SetCellValue("");
                ICell Icell8 = headerRow.CreateCell(8);
                Icell8.CellStyle = SetCellStyle(workbook);
                Icell8.SetCellValue("");
                ICell Icell9 = headerRow.CreateCell(6);
                Icell9.CellStyle = SetCellStyle(workbook);
                Icell9.SetCellValue("");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 9));

                ICell I2cell = header2Row.CreateCell(0);
                I2cell.CellStyle = SetCellStyle1(workbook);
                I2cell.SetCellValue("");
                ICell I2cell1 = header2Row.CreateCell(1);
                I2cell1.CellStyle = SetCellStyle1(workbook);
                I2cell1.SetCellValue("");
                ICell I2cell2 = header2Row.CreateCell(2);
                I2cell2.CellStyle = SetCellStyle1(workbook);
                I2cell2.SetCellValue("");
                ICell I2cell3 = header2Row.CreateCell(3);
                I2cell3.CellStyle = SetCellStyle1(workbook);
                I2cell3.SetCellValue("");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, 3));
                ICell I2cell4 = header2Row.CreateCell(4);
                I2cell4.CellStyle = SetCellStyle1(workbook);
                I2cell4.SetCellValue("");
                ICell I2cell5 = header2Row.CreateCell(5);
                I2cell5.CellStyle = SetCellStyle1(workbook);
                I2cell5.SetCellValue("");
                ICell I2cell6 = header2Row.CreateCell(6);
                I2cell6.CellStyle = SetCellStyle1(workbook);
                I2cell6.SetCellValue("");

                ICell I2cell7 = header2Row.CreateCell(7);
                I2cell7.CellStyle = SetCellStyle1(workbook);
                I2cell7.SetCellValue("");
                ICell I2cell8 = header2Row.CreateCell(8);
                I2cell8.CellStyle = SetCellStyle1(workbook);
                I2cell8.SetCellValue("");
                ICell I2cell9 = header2Row.CreateCell(9);
                I2cell9.CellStyle = SetCellStyle1(workbook);
                I2cell9.SetCellValue("");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 4, 9));

                ICell I4cell = header3Row.CreateCell(0);
                I4cell.CellStyle = SetCellStyle1(workbook);
                I4cell.SetCellValue("");
                ICell I4cell1 = header3Row.CreateCell(1);
                I4cell1.CellStyle = SetCellStyle1(workbook);
                I4cell1.SetCellValue("");
                ICell I4cell2 = header3Row.CreateCell(2);
                I4cell2.CellStyle = SetCellStyle1(workbook);
                I4cell2.SetCellValue("");
                ICell I4cell3 = header3Row.CreateCell(3);
                I4cell3.CellStyle = SetCellStyle1(workbook);
                I4cell3.SetCellValue("");
                ICell I4cell4 = header3Row.CreateCell(4);
                I4cell4.CellStyle = SetCellStyle1(workbook);
                I4cell4.SetCellValue("");
                ICell I4cell5 = header3Row.CreateCell(5);
                I4cell5.CellStyle = SetCellStyle1(workbook);
                I4cell5.SetCellValue("");
                ICell I4cell6 = header3Row.CreateCell(6);
                I4cell6.CellStyle = SetCellStyle1(workbook);
                I4cell6.SetCellValue("");

                ICell I4cell7 = header3Row.CreateCell(7);
                I4cell7.CellStyle = SetCellStyle1(workbook);
                I4cell7.SetCellValue("");
                ICell I4cell8 = header3Row.CreateCell(8);
                I4cell8.CellStyle = SetCellStyle1(workbook);
                I4cell8.SetCellValue("");
                ICell I4cell9 = header3Row.CreateCell(9);
                I4cell9.CellStyle = SetCellStyle1(workbook);
                I4cell9.SetCellValue("");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, 9));



                ICell I5cell = header4Row.CreateCell(0);
                I5cell.CellStyle = SetCellStyle1(workbook);
                I5cell.SetCellValue("");
                ICell I5cell1 = header4Row.CreateCell(1);
                I5cell1.CellStyle = SetCellStyle1(workbook);
                I5cell1.SetCellValue("");
                ICell I5cell2 = header4Row.CreateCell(2);
                I5cell2.CellStyle = SetCellStyle1(workbook);
                I5cell2.SetCellValue("");
                ICell I5cell3 = header4Row.CreateCell(3);
                I5cell3.CellStyle = SetCellStyle1(workbook);
                I5cell3.SetCellValue("");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 0, 3));
                ICell I5cell4 = header4Row.CreateCell(4);
                I5cell4.CellStyle = SetCellStyle1(workbook);
                I5cell4.SetCellValue("");
                ICell I5cell5 = header4Row.CreateCell(5);
                I5cell5.CellStyle = SetCellStyle1(workbook);
                I5cell5.SetCellValue("");
                ICell I5cell6 = header4Row.CreateCell(6);
                I5cell6.CellStyle = SetCellStyle1(workbook);
                I5cell6.SetCellValue("");

                ICell I5cell7 = header4Row.CreateCell(7);
                I5cell7.CellStyle = SetCellStyle1(workbook);
                I5cell7.SetCellValue("");
                ICell I5cell8 = header4Row.CreateCell(8);
                I5cell8.CellStyle = SetCellStyle1(workbook);
                I5cell8.SetCellValue("");
                ICell I5cell9 = header4Row.CreateCell(9);
                I5cell9.CellStyle = SetCellStyle1(workbook);
                I5cell9.SetCellValue("");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 4, 9));

                ICell I3cell = header5Row.CreateCell(0);
                I3cell.CellStyle = SetCellStyle2(workbook);
                I3cell.SetCellValue("名称");

                ICell I3cell1 = header5Row.CreateCell(1);
                I3cell1.CellStyle = SetCellStyle2(workbook);
                I3cell1.SetCellValue("规格");

                ICell I3cell2 = header5Row.CreateCell(2);
                I3cell2.CellStyle = SetCellStyle2(workbook);
                I3cell2.SetCellValue("单位");

                ICell I3cell3 = header5Row.CreateCell(3);
                I3cell3.CellStyle = SetCellStyle2(workbook);
                I3cell3.SetCellValue("数量");

                ICell I3cell4 = header5Row.CreateCell(4);
                I3cell4.CellStyle = SetCellStyle2(workbook);
                I3cell4.SetCellValue("单价");
                ICell I3cell5 = header5Row.CreateCell(5);
                I3cell5.CellStyle = SetCellStyle2(workbook);
                I3cell5.SetCellValue("金额");
                ICell I3cell6 = header5Row.CreateCell(6);
                I3cell6.CellStyle = SetCellStyle2(workbook);
                I3cell6.SetCellValue("税前单价");
                ICell I3cell7 = header5Row.CreateCell(7);
                I3cell7.CellStyle = SetCellStyle2(workbook);
                I3cell7.SetCellValue("税前金额");
                ICell I3cell8 = header5Row.CreateCell(8);
                I3cell8.CellStyle = SetCellStyle2(workbook);
                I3cell8.SetCellValue("结账方式");
                ICell I3cell9 = header5Row.CreateCell(9);
                I3cell9.CellStyle = SetCellStyle2(workbook);
                I3cell9.SetCellValue("备注");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 0, 0));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 0, 0));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 4, 4));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 5, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 6, 6));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 7, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 8, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 9, 9));

                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色
                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色
                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色
                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充



                //    //set date format  
                //ICellStyle cellStyleDate = workbook.CreateCellStyle();
                //IDataFormat format = workbook.CreateDataFormat();
                //cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");

                //IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行

                //ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列


                //cellTitle.SetCellValue(a_strTitle);

                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, a_strCols.Length - 1));

                //cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                //cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //使用NPOI操作Excel表  
                //NPOI.SS.UserModel.IRow row = sheet.CreateRow(1);
                int count = 0;
                //for (int i = 0; i < a_strCols.Length; i++)//生成sheet第一行列名  
                //{
                //    sheet.SetColumnWidth(i, Convert.ToInt32(a_strCols[i].Split('-')[1]));
                //    NPOI.SS.UserModel.ICell cell = row.CreateCell(count++);
                //    cell.SetCellValue(a_strCols[i].Split('-')[0]);
                //    //cell.CellStyle = CellStyleContent(workbook);
                //    cell.CellStyle = mHSSFCellStyle;
                //}
                //将数据导入到excel表中  


                //for (int i = 0; i < dts.Rows.Count; i++)
                //{
                //    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 1);
                //    count = 0;
                //    for (int j = 0; j < 2; j++)
                //    {
                //        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                //        Type type = dts.Rows[i][j].GetType();
                //        cell.SetCellValue(dts.Rows[i][j].ToString());
                //        //cell.CellStyle = CellStyleContent(workbook);
                //        cell.CellStyle = mHSSFCellStyle;
                //    }
                //}

                //for (int i = 0; i < dts.Rows.Count; i++)
                //{
                //    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 2);
                //    count = 0;
                //    for (int j = 2; j < 3; j++)
                //    {
                //        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                //        Type type = dts.Rows[i][j].GetType();
                //        cell.SetCellValue(dts.Rows[i][j].ToString());
                //        //cell.CellStyle = CellStyleContent(workbook);
                //        cell.CellStyle = mHSSFCellStyle;
                //    }
                //}

                //for (int i = 0; i < dts.Rows.Count; i++)
                //{
                //    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 3);
                //    count = 0;
                //    for (int j = 3; j < 5; j++)
                //    {
                //        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                //        Type type = dts.Rows[i][j].GetType();
                //        cell.SetCellValue(dts.Rows[i][j].ToString());
                //        //cell.CellStyle = CellStyleContent(workbook);
                //        cell.CellStyle = mHSSFCellStyle;
                //    }
                //}
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 5);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }


        /// <summary>
        /// 本月销售 将DataSet数据集转换HSSFworkbook对象，并保存为Stream流  
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="a_strTitle"></param>
        /// <param name="a_strCols"></param>
        public static MemoryStream ExportDataTableToExcelBYSales(DataTable dt, string a_strTitle, string[] a_strCols, string StartDate, string EndDate)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色
                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色
                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色
                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色
                //自动换行  不自动换行 
                //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, a_strCols.Length - 1));

                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(0);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;
                //topcelll.CellStyle = HorizontalAlignment.RIGHT;
                //topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT; //HorizontalAlignment.RIGHT;
                //topcelll.CellStyle.VerticalAlignment = ;
                topcelll.CellStyle = mHSSFCellStyle;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, a_strCols.Length - 1));
                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;

                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;
                for (int i = 0; i < a_strCols.Length; i++)//生成sheet第一行列名  
                {
                    sheet.SetColumnWidth(i, Convert.ToInt32(a_strCols[i].Split('-')[1]));
                    NPOI.SS.UserModel.ICell cell = row.CreateCell(count++);
                    cell.SetCellValue(a_strCols[i].Split('-')[0]);
                    //cell.CellStyle = CellStyleContent(workbook);
                    cell.CellStyle = mHSSFCellStyle;
                }


                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 3);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }


                //

                IRow bottomrow = sheet.CreateRow(dt.Rows.Count + 4);//在工作表中添加一行
                IRow bottomrow1 = sheet.CreateRow(dt.Rows.Count + 5);
                ICell bottomcell = bottomrow.CreateCell(a_strCols.Length - 2);//在行中添加一列
                ICell bottomcell1 = bottomrow1.CreateCell(a_strCols.Length - 2);
                bottomcell.SetCellValue("燕山输配产品部");
                bottomcell1.SetCellValue("截止日期：" + StartDate+"-"+EndDate);
                bottomcell.CellStyle = mHSSFCellStyle;
                bottomcell1.CellStyle = mHSSFCellStyle;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, a_strCols.Length - 2, a_strCols.Length - 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 5, dt.Rows.Count + 5, a_strCols.Length - 2, a_strCols.Length - 1));
                //



                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }




        /// <summary>
        /// 合同额分析
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="a_strTitle"></param>
        /// <returns></returns>
        public static MemoryStream ContractStatisticalAnalysisToExcel(DataTable dt, DataTable dt2, string a_strTitle, string Datetime)
        {


            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 9));
                cellTitle.CellStyle = mHSSFCellStyle;
                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(7);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;
                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("本月");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("上月");
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("环比");
                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue("去年同期");
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("同比");

                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("占全年累计合同额%");

                NPOI.SS.UserModel.ICell cell8 = row.CreateCell(9);
                sheet.SetColumnWidth(9, Convert.ToInt32(5000));
                cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell8.SetCellValue("备注");



                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 1, 1));
                //NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                // int count2 = 2;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 3, 4));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 0, 0));


                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 5, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 6, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 8, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 9, 9));
                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                sheet.SetColumnWidth(3, 5000);
                NPOI.SS.UserModel.ICell r3cell2 = row3.CreateCell(3);
                r3cell2.SetCellValue("变动");
                sheet.SetColumnWidth(3, 5000);
                ICell r3cell3 = row3.CreateCell(4);
                r3cell3.SetCellValue("变动%");
                sheet.SetColumnWidth(4, 5000);
                //cell3.CellStyle = mHSSFCellStyle;
                ICell r3cell4 = row3.CreateCell(6);
                r3cell4.SetCellValue("变动");
                sheet.SetColumnWidth(6, 5000);
                // cell4.CellStyle = mHSSFCellStyle;
                //ICell r3cell5 = row3.CreateCell(5);
                //r3cell5.SetCellValue("合同额");
                ////cell5.CellStyle = mHSSFCellStyle;
                ICell r3cell6 = row3.CreateCell(7);
                sheet.SetColumnWidth(7, 5000);
                r3cell6.SetCellValue("变动%");
                //// cell6.CellStyle = mHSSFCellStyle;
                //sheet.SetColumnWidth(7, 5000);
                //ICell r3cell7 = row3.CreateCell(7);
                //r3cell7.SetCellValue("毛利润");
                ////  cell7.CellStyle = mHSSFCellStyle;
                // sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 2, 3));
                // sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 7));


                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 4);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                //

                //IRow bottomrow = sheet.CreateRow(dt.Rows.Count + 4);//在工作表中添加一行
                //IRow bottomrow1 = sheet.CreateRow(dt.Rows.Count + 5);
                //ICell bottomcell = bottomrow.CreateCell(6);//在行中添加一列
                //ICell bottomcell1 = bottomrow1.CreateCell(6);
                //bottomcell.SetCellValue("燕山输配产品部");
                //bottomcell1.SetCellValue("截止日期：" + Datetime);

                //IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                //在行中添加一列
                IRow row4 = sheet.CreateRow(dt.Rows.Count + 4);
                ICell r4cellTitle = row4.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r4cellTitle.SetCellValue("1-10月累计合同总额统计表");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 0, 9));
                r4cellTitle.CellStyle = mHSSFCellStyle;
                IRow r4toprow = sheet.CreateRow(dt.Rows.Count + 5);
                ICell r4topcelll = r4toprow.CreateCell(7);
                r4topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row5 = sheet.CreateRow(dt.Rows.Count + 6);
                int count2 = 0;
                NPOI.SS.UserModel.ICell r5cell1 = row5.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r5cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell r5cell2 = row5.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r5cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell2.SetCellValue("1-10月");
                NPOI.SS.UserModel.ICell r5cell3 = row5.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                r5cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell3.SetCellValue("去年同期");
                NPOI.SS.UserModel.ICell r5cell4 = row5.CreateCell(3);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                r5cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell4.SetCellValue("同比");
                NPOI.SS.UserModel.ICell r5cell5 = row5.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                r5cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell5.SetCellValue("备注");



                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 1, 1));
                //NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                // int count2 = 2;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 3, 4));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 0, 0));


                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 5, 5));
                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 5, dt.Rows.Count + 6, 6, 7));
                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 5, 8, 8));
                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 5, 9, 9));

                NPOI.SS.UserModel.IRow row6 = sheet.CreateRow(dt.Rows.Count + 7);
                sheet.SetColumnWidth(3, 5000);
                NPOI.SS.UserModel.ICell r6cell2 = row6.CreateCell(3);
                r6cell2.SetCellValue("变动");
                sheet.SetColumnWidth(3, 5000);
                ICell r6cell3 = row6.CreateCell(4);
                r6cell3.SetCellValue("变动%");
                sheet.SetColumnWidth(4, 5000);

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + 8);
                    count = 0;
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt2.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt2.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt2.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt2.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt2.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt2.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }




                //bottomcell.CellStyle = mHSSFCellStyle;
                // bottomcell1.CellStyle = mHSSFCellStyle;
                //  sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 6, 7));
                // sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 5, dt.Rows.Count + 5, 6, 7));
                //

                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }

        }
        /// <summary>
        /// 应收账款统计分析
        /// </summary>
        /// <returns></returns>

        public static MemoryStream DeceiveAccountTableToExcel(DataTable dt, DataTable dt2, DataTable dt3, string a_strTitle, string Datetime)
        {

            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 9));
                cellTitle.CellStyle = mHSSFCellStyle;
                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(7);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;
                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("已收款本月");
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("总回款率");
                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(8000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue("是否按合同约定进度回款（是、否）");
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(8000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("本月应收帐款额（无填0）");

                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("同比增减%%");

                NPOI.SS.UserModel.ICell cell8 = row.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell8.SetCellValue("占本月公司应收账款总额%");
                //备注
                NPOI.SS.UserModel.ICell cell9 = row.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell9.SetCellValue("备注");
                //将数据导入到excel表中  
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 3);
                        count = 0;
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                            Type type = dt.Rows[i][j].GetType();
                            if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                            {
                                cell.SetCellValue((int)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                                {
                                    cell.SetCellValue((Double)dt.Rows[i][j]);
                                }
                                else
                                {
                                    if (type == typeof(DateTime))
                                    {
                                        cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                    }
                                    else
                                    {
                                        if (type == typeof(bool) || type == typeof(Boolean))
                                        {
                                            cell.SetCellValue((bool)dt.Rows[i][j]);
                                        }
                                        else
                                        {
                                            cell.SetCellValue(dt.Rows[i][j].ToString());
                                        }
                                    }
                                }
                            }
                            //cell.CellStyle = CellStyleContent(workbook);
                            // cell.CellStyle = mHSSFCellStyle;
                        }
                    }
                }


                var dt2count = 0;
                var dtcount = 0;
                if (dt2 != null) { dt2count = dt2.Rows.Count; }
                else
                {
                    dt2count = 8;
                }
                if (dt != null)
                {
                    dtcount = dt.Rows.Count;
                }
                else
                {
                    dtcount = 8;
                }
                //在行中添加一列
                IRow row4 = sheet.CreateRow(dtcount + 6);
                ICell r4cellTitle = row4.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r4cellTitle.SetCellValue("本月应收账款结构（单位：万元）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dtcount + 6, dtcount + 6, 0, 9));
                r4cellTitle.CellStyle = mHSSFCellStyle;
                IRow r4toprow = sheet.CreateRow(dtcount + 7);
                ICell r4topcelll = r4toprow.CreateCell(7);
                r4topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row5 = sheet.CreateRow(dtcount + 8);
                int count2 = 0;
                NPOI.SS.UserModel.ICell r5cell1 = row5.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r5cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell r5cell2 = row5.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r5cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell2.SetCellValue("项目/产品");
                NPOI.SS.UserModel.ICell r5cell3 = row5.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                r5cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell3.SetCellValue("客户名称");
                NPOI.SS.UserModel.ICell r5cell4 = row5.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                r5cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell4.SetCellValue("应收款额");
                NPOI.SS.UserModel.ICell r5cell5 = row5.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                r5cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell5.SetCellValue("形成原因");
                NPOI.SS.UserModel.ICell r5cell6 = row5.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                r5cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell6.SetCellValue("收回风险");
                NPOI.SS.UserModel.ICell r5cell7 = row5.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                r5cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell7.SetCellValue("占当月应收账款总额%");
                NPOI.SS.UserModel.ICell r5cell8 = row5.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                r5cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell8.SetCellValue("其他需要说明的内容");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dtcount + 8, dtcount + 9, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dtcount + 8, dtcount + 9, 1, 1));
                //NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                // int count2 = 2;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dtcount + 8, dtcount + 9, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dtcount + 8, dtcount + 9, 5, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dtcount + 8, dtcount + 9, 6, 6));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dtcount + 8, dtcount + 9, 7, 7));
                //  sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 4, 4));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dtcount + 8, dtcount + 9, 0, 0));
                NPOI.SS.UserModel.IRow row6 = sheet.CreateRow(dtcount + 9);
                sheet.SetColumnWidth(4, 5000);
                NPOI.SS.UserModel.ICell r6cell2 = row6.CreateCell(4);
                r6cell2.SetCellValue("（何时收回）");
               // sheet.SetColumnWidth(3, 5000);
                if (dt2 != null)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dtcount + 10);
                        count = 0;
                        for (int j = 0; j < dt2.Columns.Count; j++)
                        {
                            NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                            Type type = dt2.Rows[i][j].GetType();
                            if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                            {
                                cell.SetCellValue((int)dt2.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                                {
                                    cell.SetCellValue((Double)dt2.Rows[i][j]);
                                }
                                else
                                {
                                    if (type == typeof(DateTime))
                                    {
                                        cell.SetCellValue(((DateTime)dt2.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                    }
                                    else
                                    {
                                        if (type == typeof(bool) || type == typeof(Boolean))
                                        {
                                            cell.SetCellValue((bool)dt2.Rows[i][j]);
                                        }
                                        else
                                        {
                                            cell.SetCellValue(dt2.Rows[i][j].ToString());
                                        }
                                    }
                                }
                            }
                            //cell.CellStyle = CellStyleContent(workbook);
                            // cell.CellStyle = mHSSFCellStyle;
                        }
                    }
                }
                ///
               
                IRow Rrow6 = sheet.CreateRow(dt2count+ dtcount + 12);
                ICell r6cellTitle = Rrow6.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r6cellTitle.SetCellValue("累计应收账款情况（单位：万元）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2count + dtcount + 12, dt2count + dtcount + 12, 0, 9));
                r6cellTitle.CellStyle = mHSSFCellStyle;
                //IRow r7toprow = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 13);
                //ICell r7topcelll = r4toprow.CreateCell(7);
                //r4topcelll.SetCellValue("单位：万元");
                //mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row7 = sheet.CreateRow(dt2count + dtcount + 13);
                //int count2 = 0;
                NPOI.SS.UserModel.ICell r7cell1 = row7.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r7cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell r7cell2 = row7.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r7cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell2.SetCellValue("项目/产品");
                NPOI.SS.UserModel.ICell r7cell3 = row7.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                r7cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell3.SetCellValue("客户名称");
                NPOI.SS.UserModel.ICell r7cell4 = row7.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                r7cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell4.SetCellValue("应收款额");





                NPOI.SS.UserModel.ICell r7cell5 = row7.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                r7cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell5.SetCellValue("账龄");
                NPOI.SS.UserModel.ICell r7cell6 = row7.CreateCell(9);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                r7cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell6.SetCellValue("是否在合同约定期内");
                NPOI.SS.UserModel.ICell r6cell7 = row7.CreateCell(10);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                r6cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r6cell7.SetCellValue("备注");
                //NPOI.SS.UserModel.ICell r7cell8 = row7.CreateCell(7);
                //sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                //r7cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //r7cell8.SetCellValue("其他需要说明的内容");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2count + dtcount + 13, dt2count + dtcount + 14, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2count + dtcount + 13, dt2count + dtcount + 14, 1, 1));
                //NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                // int count2 = 2;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2count + dtcount + 13, dt2count + dtcount + 14, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2count + dtcount + 13, dt2count + dtcount + 13, 4, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2count + dtcount + 13, dt2count + dtcount + 14, 9, 9));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2count + dtcount + 13, dt2count + dtcount + 14, 10, 10));
                //  sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 4, 4));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2count + dtcount + 13, dt2count + dtcount + 14, 0, 0));
                NPOI.SS.UserModel.IRow row8 = sheet.CreateRow(dt2count + dtcount + 14);
                //sheet.SetColumnWidth(4, 5000);
                //NPOI.SS.UserModel.ICell r8cell2 = row6.CreateCell(4);
                //r8cell2.SetCellValue("（何时收回）");



                sheet.SetColumnWidth(4, 5000);
                NPOI.SS.UserModel.ICell r8cell3 = row8.CreateCell(4);
                r8cell3.SetCellValue("＜3月");
                // sheet.SetColumnWidth(3, 5000);
                sheet.SetColumnWidth(4, 5000);
                NPOI.SS.UserModel.ICell r8cell4 = row8.CreateCell(5);
                sheet.SetColumnWidth(5, 5000);
                r8cell4.SetCellValue("≥3，＜6月");
                NPOI.SS.UserModel.ICell r8cell5 = row8.CreateCell(6);
                sheet.SetColumnWidth(6, 5000);
                r8cell5.SetCellValue("≥6，＜1年");
                NPOI.SS.UserModel.ICell r8cell6 = row8.CreateCell(7);
                sheet.SetColumnWidth(7, 5000);
                r8cell6.SetCellValue("≥1，＜2年");
                NPOI.SS.UserModel.ICell r8cell7 = row8.CreateCell(8);
                sheet.SetColumnWidth(8, 5000);
                r8cell7.SetCellValue("≥2年");
                if (dt3 != null)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt2count + dtcount + 15);
                        count = 0;
                        for (int j = 0; j < dt3.Columns.Count; j++)
                        {
                            NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                            Type type = dt3.Rows[i][j].GetType();
                            if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                            {
                                cell.SetCellValue((int)dt3.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                                {
                                    cell.SetCellValue((Double)dt3.Rows[i][j]);
                                }
                                else
                                {
                                    if (type == typeof(DateTime))
                                    {
                                        cell.SetCellValue(((DateTime)dt3.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                    }
                                    else
                                    {
                                        if (type == typeof(bool) || type == typeof(Boolean))
                                        {
                                            cell.SetCellValue((bool)dt3.Rows[i][j]);
                                        }
                                        else
                                        {
                                            cell.SetCellValue(dt3.Rows[i][j].ToString());
                                        }
                                    }
                                }
                            }
                            //cell.CellStyle = CellStyleContent(workbook);
                            // cell.CellStyle = mHSSFCellStyle;
                        }
                    }


                }

                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }


       /// <summary>
       /// 应付款经营分析二
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="dt2"></param>
       /// <param name="dt3"></param>
       /// <param name="a_strTitle"></param>
       /// <param name="Datetime"></param>
       /// <returns></returns>

        public static MemoryStream AccountsPayableStatisticalAnalysis2ToExcel(DataTable dt, DataTable dt2, DataTable dt3, string a_strTitle, string Datetime)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 9));
                cellTitle.CellStyle = mHSSFCellStyle;
                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(7);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;

                //部门	负责人	项目名称	合计金额	已付款	未付款	本月应付额（无填0）	实付额	占本月公司应付账款总额%	备注

                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("负责人");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("合计金额");
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("已付款");
                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(8000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue("未付款");
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(8000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("本月应付额");

                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("实付款额");
                NPOI.SS.UserModel.ICell cell8 = row.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell8.SetCellValue("占本月公司应收账款总额%");

                NPOI.SS.UserModel.ICell cell9 = row.CreateCell(8);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell9.SetCellValue("备注");
                //备注
                //NPOI.SS.UserModel.ICell cell10 = row.CreateCell(8);
                //sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                //cell10.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //cell10.SetCellValue("");
                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 3);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                //部门	项目/产品	负责人	应付款总额	账龄	实际支付款额	其他需要说明的内容
				//（哪年）		

                //在行中添加一列
                IRow row4 = sheet.CreateRow(dt.Rows.Count + 6);
                ICell r4cellTitle = row4.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r4cellTitle.SetCellValue("本月支付往期应付账款情况（非本月应付账款范围内）（单位：万元）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 0, 9));
                r4cellTitle.CellStyle = mHSSFCellStyle;
                IRow r4toprow = sheet.CreateRow(dt.Rows.Count + 7);
                ICell r4topcelll = r4toprow.CreateCell(7);
                r4topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row5 = sheet.CreateRow(dt.Rows.Count + 8);
                int count2 = 0;
                NPOI.SS.UserModel.ICell r5cell1 = row5.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r5cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell r5cell2 = row5.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r5cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell2.SetCellValue("项目/产品");
                NPOI.SS.UserModel.ICell r5cell3 = row5.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                r5cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell3.SetCellValue("负责人");
                NPOI.SS.UserModel.ICell r5cell4 = row5.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                r5cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell4.SetCellValue("应付款总额");
                NPOI.SS.UserModel.ICell r5cell5 = row5.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                r5cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell5.SetCellValue("账龄");
                NPOI.SS.UserModel.ICell r5cell6 = row5.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                r5cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell6.SetCellValue("实际支付款额");
                NPOI.SS.UserModel.ICell r5cell7 = row5.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                r5cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell7.SetCellValue("其他需要说明的内容");
                //NPOI.SS.UserModel.ICell r5cell8 = row5.CreateCell(7);
                //sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                //r5cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //r5cell8.SetCellValue("其他需要说明的内容");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 8, dt.Rows.Count + 9, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 8, dt.Rows.Count + 9, 1, 1));
                //NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                // int count2 = 2;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 8, dt.Rows.Count + 9, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 8, dt.Rows.Count + 9, 5, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 8, dt.Rows.Count + 9, 6, 6));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 8, dt.Rows.Count + 9, 7, 7));
                //  sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 4, 4));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 8, dt.Rows.Count + 9, 0, 0));
                NPOI.SS.UserModel.IRow row6 = sheet.CreateRow(dt.Rows.Count + 9);
                sheet.SetColumnWidth(4, 5000);
                NPOI.SS.UserModel.ICell r6cell2 = row6.CreateCell(4);
                r6cell2.SetCellValue("（哪年）");
                // sheet.SetColumnWidth(3, 5000);

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + 10);
                    count = 0;
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt2.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt2.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt2.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt2.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt2.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt2.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }
                ///
                IRow Rrow6 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 12);
                ICell r6cellTitle = Rrow6.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r6cellTitle.SetCellValue("累计历年应付账款情况（单位：万元）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2.Rows.Count + dt.Rows.Count + 12, dt2.Rows.Count + dt.Rows.Count + 12, 0, 9));
                r6cellTitle.CellStyle = mHSSFCellStyle;
                //IRow r7toprow = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 13);
                //ICell r7topcelll = r4toprow.CreateCell(7);
                //r4topcelll.SetCellValue("单位：万元");
                //mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row7 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 13);
                //int count2 = 0;
                NPOI.SS.UserModel.ICell r7cell1 = row7.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r7cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell r7cell2 = row7.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r7cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell2.SetCellValue("年份");
                NPOI.SS.UserModel.ICell r7cell3 = row7.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                r7cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell3.SetCellValue("应付款额");
                NPOI.SS.UserModel.ICell r7cell4 = row7.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                r7cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell4.SetCellValue("占历年累计应付款总额%");





                //NPOI.SS.UserModel.ICell r7cell5 = row7.CreateCell(4);
                //sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                //r7cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //r7cell5.SetCellValue("账龄");
                //NPOI.SS.UserModel.ICell r7cell6 = row7.CreateCell(9);
                //sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                //r7cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //r7cell6.SetCellValue("是否在合同约定期内");
                NPOI.SS.UserModel.ICell r6cell7 = row7.CreateCell(10);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                r6cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r6cell7.SetCellValue("备注");
                //NPOI.SS.UserModel.ICell r7cell8 = row7.CreateCell(7);
                //sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                //r7cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //r7cell8.SetCellValue("其他需要说明的内容");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 1, 1));
                //NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                // int count2 = 2;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 13, 4, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 9, 9));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 10, 10));
                //  sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 4, 4));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 0, 0));
                NPOI.SS.UserModel.IRow row8 = sheet.CreateRow(dt.Rows.Count + dt2.Rows.Count + 14);
                //sheet.SetColumnWidth(4, 5000);
                //NPOI.SS.UserModel.ICell r8cell2 = row6.CreateCell(4);
                //r8cell2.SetCellValue("（何时收回）");



                sheet.SetColumnWidth(4, 5000);
                NPOI.SS.UserModel.ICell r8cell3 = row8.CreateCell(4);
                r8cell3.SetCellValue("哪年");
                // sheet.SetColumnWidth(3, 5000);
                sheet.SetColumnWidth(4, 5000);
                //NPOI.SS.UserModel.ICell r8cell4 = row8.CreateCell(5);
                //sheet.SetColumnWidth(5, 5000);
                //r8cell4.SetCellValue("≥3，＜6月");
                //NPOI.SS.UserModel.ICell r8cell5 = row8.CreateCell(6);
                //sheet.SetColumnWidth(6, 5000);
                //r8cell5.SetCellValue("≥6，＜1年");
                //NPOI.SS.UserModel.ICell r8cell6 = row8.CreateCell(7);
                //sheet.SetColumnWidth(7, 5000);
                //r8cell6.SetCellValue("≥1，＜2年");
                //NPOI.SS.UserModel.ICell r8cell7 = row8.CreateCell(8);
                //sheet.SetColumnWidth(8, 5000);
                //r8cell7.SetCellValue("≥2年");
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + dt2.Rows.Count + 15);
                    count = 0;
                    for (int j = 0; j < dt3.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt3.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt3.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt3.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt3.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt3.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt3.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }




                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }



        /// <summary>
        /// 自有产品的销售量和销售额
        /// </summary>
        /// <returns></returns>
        public static MemoryStream MonthOwnProductSalesToExcel(DataTable dt, DataTable dt2, DataTable dt3,DataTable dt4,string a_strTitle, string Datetime) 
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue("本月自有产品销售数量（单位：台）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 9));
                cellTitle.CellStyle = mHSSFCellStyle;
              
                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;

                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("产品分类");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("当月");
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("上月");
                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(8000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue("环比");
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(8000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("去年同期");

                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(8000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("同比");



                NPOI.SS.UserModel.ICell cell8 = row.CreateCell(9);
                sheet.SetColumnWidth(9, Convert.ToInt32(5000));
                cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell8.SetCellValue("占全年累计销售量%");
                NPOI.SS.UserModel.ICell cell9 = row.CreateCell(10);
                sheet.SetColumnWidth(10, Convert.ToInt32(5000));
                cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell9.SetCellValue("备注");

                NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(3);
                NPOI.SS.UserModel.ICell r2cell2= row2.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                r2cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r2cell2.SetCellValue("变动");
                NPOI.SS.UserModel.ICell r2cell3 = row2.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                r2cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r2cell3.SetCellValue("变动%");
                NPOI.SS.UserModel.ICell r2cell4 = row2.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                r2cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r2cell4.SetCellValue("变动");
                NPOI.SS.UserModel.ICell r2cell5= row2.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                r2cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r2cell5.SetCellValue("变动%");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 0, 0));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 3,3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 6, 6));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 7, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 9, 9)); 
            //    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 3, 3));


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 4);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                //在行中添加一列
                IRow row4 = sheet.CreateRow(dt.Rows.Count + 6);
                ICell r4cellTitle = row4.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r4cellTitle.SetCellValue("本月自有产品销售额/合同额(单位:万元)");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 0, 9));
                r4cellTitle.CellStyle = mHSSFCellStyle;
                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row5 = sheet.CreateRow(dt.Rows.Count +7);
                int count2 = 0;
                NPOI.SS.UserModel.ICell r5cell1 = row5.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r5cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell r5cell2 = row5.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r5cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell2.SetCellValue("产品分类");
                NPOI.SS.UserModel.ICell R5cell3 = row5.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                R5cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell3.SetCellValue("当月");
                NPOI.SS.UserModel.ICell R5cell4 = row5.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                R5cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell4.SetCellValue("上月");
                NPOI.SS.UserModel.ICell R5cell5 = row5.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(8000));
                R5cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell5.SetCellValue("环比");
                NPOI.SS.UserModel.ICell R5cell6 = row5.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(8000));
                R5cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell6.SetCellValue("去年同期");

                NPOI.SS.UserModel.ICell R5cell7 = row5.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(8000));
                R5cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell7.SetCellValue("同比");



                NPOI.SS.UserModel.ICell R5cell8 = row5.CreateCell(9);
                sheet.SetColumnWidth(9, Convert.ToInt32(5000));
                R5cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell8.SetCellValue("占全年累计销售量%");
                NPOI.SS.UserModel.ICell R5cell9 = row5.CreateCell(10);
                sheet.SetColumnWidth(10, Convert.ToInt32(5000));
                R5cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell9.SetCellValue("备注");

                NPOI.SS.UserModel.IRow row6 = sheet.CreateRow(dt.Rows.Count + 8);
                NPOI.SS.UserModel.ICell R6cell2 = row6.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R6cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R6cell2.SetCellValue("变动");
                NPOI.SS.UserModel.ICell R6cell3 = row6.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                R6cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R6cell3.SetCellValue("变动%");
                NPOI.SS.UserModel.ICell R6cell4 = row6.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                R6cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R6cell4.SetCellValue("变动");
                NPOI.SS.UserModel.ICell R6cell5 = row6.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                R6cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R6cell5.SetCellValue("变动%");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 7, dt.Rows.Count + 8, 0, 0));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 7, dt.Rows.Count + 8, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 7, dt.Rows.Count + 8, 2, 2));
                // int count2 = 2;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 7, dt.Rows.Count + 8, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 7, dt.Rows.Count + 7, 4, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 7, dt.Rows.Count + 8, 6, 6));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 7, dt.Rows.Count + 7, 7, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 7, dt.Rows.Count + 8, 9, 9));
              
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + 10);
                    count = 0;
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt2.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt2.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt2.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt2.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt2.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt2.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }
                ///
                IRow Rrow6 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 12);
                ICell r6cellTitle = Rrow6.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r6cellTitle.SetCellValue("1-？月累计自有产品销售量(单位:台)");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2.Rows.Count + dt.Rows.Count + 12, dt2.Rows.Count + dt.Rows.Count + 12, 0, 9));
                r6cellTitle.CellStyle = mHSSFCellStyle;
                r6cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                r6cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //r6cellTitle.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row7 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 13);
                //int count2 = 0;
                NPOI.SS.UserModel.ICell r7cell1 = row7.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r7cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell r7cell2 = row7.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r7cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell2.SetCellValue("产品分类");
                NPOI.SS.UserModel.ICell r7cell3 = row7.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                r7cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell3.SetCellValue("1-？月");
                NPOI.SS.UserModel.ICell r7cell4 = row7.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                r7cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell4.SetCellValue("去年同期");

                NPOI.SS.UserModel.ICell r7cell5 = row7.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                r7cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell5.SetCellValue("同比");


                NPOI.SS.UserModel.ICell r7cell6 = row7.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                r7cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell6.SetCellValue("占全年累计销售量%");

                NPOI.SS.UserModel.ICell r7cell7= row7.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                r7cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell7.SetCellValue("备注");


                NPOI.SS.UserModel.IRow Row8 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 14);
                NPOI.SS.UserModel.ICell r8cell = Row8.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                r8cell.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r8cell.SetCellValue("变动");

                NPOI.SS.UserModel.ICell r8cell2 = Row8.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                r8cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r8cell2.SetCellValue("变动%");


               
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 0, 0));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 13, 4,5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 6,6));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + dt2.Rows.Count + 13, dt.Rows.Count + dt2.Rows.Count + 14, 7, 7));
                //  sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 7, 4, 4));
           
                

                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + dt2.Rows.Count + 15);
                    count = 0;
                    for (int j = 0; j < dt3.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt3.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt3.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt3.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt3.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt3.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt3.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                IRow R9row6 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + 21);
                ICell r9cellTitle = R9row6.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r9cellTitle.SetCellValue("1-？月累计自有产品销售额(单位:万元)");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2.Rows.Count + dt.Rows.Count  + dt3.Rows.Count + 21, dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + 21, 0, 9));
                r9cellTitle.CellStyle = mHSSFCellStyle;
                r9cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                r9cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
              //  topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow Row10 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + 22);
                //int count2 = 0;
                NPOI.SS.UserModel.ICell R10cell1 = Row10.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                R10cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell R10cell2 = Row10.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                R10cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell2.SetCellValue("产品分类");
                NPOI.SS.UserModel.ICell R10cell3 = Row10.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                R10cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell3.SetCellValue("1-？月");
                NPOI.SS.UserModel.ICell R10cell4 = Row10.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                R10cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell4.SetCellValue("去年同期");

                NPOI.SS.UserModel.ICell R10cell5 = Row10.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R10cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell5.SetCellValue("同比");
                //直接成本	毛利	毛利率
                NPOI.SS.UserModel.ICell R11cell6 = Row10.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                R11cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell6.SetCellValue("直接成本");

                NPOI.SS.UserModel.ICell R11cell7 = Row10.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                R11cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell7.SetCellValue("毛利");

                NPOI.SS.UserModel.ICell R11cell8 = Row10.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                R11cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell8.SetCellValue("毛利率");

                NPOI.SS.UserModel.ICell R11cell9 = Row10.CreateCell(9);
                sheet.SetColumnWidth(9, Convert.ToInt32(5000));
                R11cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell9.SetCellValue("占全年累计销售量%");

                NPOI.SS.UserModel.ICell R11cell11 = Row10.CreateCell(10);
                sheet.SetColumnWidth(10, Convert.ToInt32(5000));
                R11cell11.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell11.SetCellValue("备注");


                NPOI.SS.UserModel.IRow Row11 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 23);
                NPOI.SS.UserModel.ICell R11cell = Row11.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R11cell.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell.SetCellValue("变动");

                NPOI.SS.UserModel.ICell R11cell2 = Row11.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                R11cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell2.SetCellValue("变动%");


                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + dt2.Rows.Count + dt3.Rows.Count + 24);
                    count = 0;
                    for (int j = 0; j < dt4.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt4.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt4.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt4.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt4.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt4.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt4.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }




                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }


        /// <summary>
        /// 自有产品渠道型号
        /// </summary>
        /// <returns></returns>
        public static MemoryStream GetMonthOwnProductChannelsFromToExcel(DataTable dt, DataTable dt2, DataTable dt3, DataTable dt4,DataTable dt5, string a_strTitle, string Datetime) 
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue("自有产品销售渠道分析");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8));
                cellTitle.CellStyle = mHSSFCellStyle;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(1);
                int count = 0;

                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("部门");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("渠道类型");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("本月销售量");
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("占本月总销售量%");
                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(8000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue("本月销售额");
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(8000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("1-？月销售量");
                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(8000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("占累计自有产品销售总量%");
                NPOI.SS.UserModel.ICell cell8 = row.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell8.SetCellValue("1-？月销售额");
                NPOI.SS.UserModel.ICell cell9 = row.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell9.SetCellValue("占累计自有产品销售总额%");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i +2);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                 
                    }
                }

                //在行中添加一列
                IRow row4 = sheet.CreateRow(dt.Rows.Count + 6);
                ICell r4cellTitle = row4.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r4cellTitle.SetCellValue("本月自有产品销售型号前10名（按销售量）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 6, dt.Rows.Count + 6, 0, 4));
                r4cellTitle.CellStyle = mHSSFCellStyle;
                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row5 = sheet.CreateRow(dt.Rows.Count + 7);
                int count2 = 0;
                NPOI.SS.UserModel.ICell r5cell1 = row5.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r5cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell r5cell2 = row5.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r5cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r5cell2.SetCellValue("产品分类");
                NPOI.SS.UserModel.ICell R5cell3 = row5.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                R5cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell3.SetCellValue("产品型号");
                NPOI.SS.UserModel.ICell R5cell4 = row5.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                R5cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell4.SetCellValue("本月销售量");
                NPOI.SS.UserModel.ICell R5cell5 = row5.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(8000));
                R5cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R5cell5.SetCellValue("占本月自有产品销售总量%");
                
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count +8);
                    count = 0;
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt2.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt2.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt2.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt2.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt2.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt2.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }
                ///
                IRow Rrow6 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 12);
                ICell r6cellTitle = Rrow6.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r6cellTitle.SetCellValue("本月自有产品销售型号前10名（按销售额）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2.Rows.Count + dt.Rows.Count + 12, dt2.Rows.Count + dt.Rows.Count + 12, 0, 4));
                r6cellTitle.CellStyle = mHSSFCellStyle;
                r6cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                r6cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //r6cellTitle.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row7 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + 13);
                //int count2 = 0;
                NPOI.SS.UserModel.ICell r7cell1 = row7.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                r7cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell r7cell2 = row7.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                r7cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell2.SetCellValue("产品分类");
                NPOI.SS.UserModel.ICell r7cell3 = row7.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                r7cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell3.SetCellValue("产品型号");
                NPOI.SS.UserModel.ICell r7cell4 = row7.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                r7cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell4.SetCellValue("本月销售额");

                NPOI.SS.UserModel.ICell r7cell5 = row7.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                r7cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                r7cell5.SetCellValue("占本月自有产品销售总额%");

                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + dt2.Rows.Count + 14);
                    count = 0;
                    for (int j = 0; j < dt3.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt3.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt3.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt3.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt3.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt3.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt3.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                IRow R9row6 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + 21);
                ICell r9cellTitle = R9row6.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r9cellTitle.SetCellValue("1-？月累计自有产品销售型号前10名（按销售量）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + 21, dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + 21, 0, 5));
                r9cellTitle.CellStyle = mHSSFCellStyle;
                r9cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                r9cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //  topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow Row10 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + 22);
                //int count2 = 0;
                NPOI.SS.UserModel.ICell R10cell1 = Row10.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                R10cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell R10cell2 = Row10.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                R10cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell2.SetCellValue("产品分类");
                NPOI.SS.UserModel.ICell R10cell3 = Row10.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                R10cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell3.SetCellValue("产品型号");
                NPOI.SS.UserModel.ICell R10cell4 = Row10.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                R10cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell4.SetCellValue("1-？月销售量");

                NPOI.SS.UserModel.ICell R10cell5 = Row10.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R10cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R10cell5.SetCellValue("占累计自有产品销售总量%");
              
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + dt2.Rows.Count + dt3.Rows.Count + 23);
                    count = 0;
                    for (int j = 0; j < dt4.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt4.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt4.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt4.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt4.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt4.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt4.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                IRow R10row6 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count +dt4.Rows.Count+ 25);
                ICell r10cellTitle = R10row6.CreateCell(0);
                //ICell r4cell0 = row4.CreateCell(0);//在行中添加一列
                r10cellTitle.SetCellValue("1-？月累计自有产品销售型号前10名（按销售额）");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + dt4.Rows.Count + 25, dt2.Rows.Count + dt.Rows.Count + dt4.Rows.Count + dt3.Rows.Count + 25, 0,4));
                r10cellTitle.CellStyle = mHSSFCellStyle;
                r10cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                r10cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //  topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow Row11 = sheet.CreateRow(dt2.Rows.Count + dt.Rows.Count + dt3.Rows.Count + dt4.Rows.Count + 26);
                //int count2 = 0;
                NPOI.SS.UserModel.ICell R11cell1 = Row11.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                R11cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell R11cell2 = Row11.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                R11cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell2.SetCellValue("产品分类");
                NPOI.SS.UserModel.ICell R11cell3 = Row11.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                R11cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell3.SetCellValue("产品型号");
                NPOI.SS.UserModel.ICell R11cell4 = Row11.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                R11cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell4.SetCellValue("1-？月销售额");

                NPOI.SS.UserModel.ICell R11cell5 = Row11.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R11cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R11cell5.SetCellValue("占累计自有产品销售总额%");

                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + dt.Rows.Count + dt2.Rows.Count + dt3.Rows.Count +dt4.Rows .Count+ 27);
                    count = 0;
                    for (int j = 0; j < dt5.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt5.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt5.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt5.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt5.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt5.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt5.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }






        /// <summary>
        /// 设备销售统计
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="a_strTitle"></param>
        /// <param name="a_strCols"></param>
        /// <returns></returns>
        public static MemoryStream EquipmentSalesSummaryTableToExcel(DataTable dt, string a_strTitle, string Start, string[] a_strTols, string End)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 7));
                cellTitle.CellStyle = mHSSFCellStyle;
                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(7);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;
                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("自由产品");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue(Start + "至" + End);
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("年度累计");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 1, 1));
                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                int count2 = 2;
                for (int i = 0; i < a_strTols.Length; i++)//生成sheet第一行列名  
                {
                    sheet.SetColumnWidth(i, Convert.ToInt32(a_strTols[i].Split('@')[1]));
                    NPOI.SS.UserModel.ICell cell = row3.CreateCell(count2++);
                    cell.SetCellValue(a_strTols[i].Split('@')[0]);
                    // cell.CellStyle = CellStyleContent(workbook);
                    // cell.CellStyle = mHSSFCellStyle;
                }
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 0, 0));

                //  NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                //  sheet.SetColumnWidth(3, 5000);
                //  NPOI.SS.UserModel.ICell cell2 = row3.CreateCell(2);
                //  cell2.SetCellValue("数量");
                //  sheet.SetColumnWidth(3, 5000);
                //  ICell cell3 = row3.CreateCell(3);
                //  cell3.SetCellValue("合同额");
                //  sheet.SetColumnWidth(4, 5000);
                //  //cell3.CellStyle = mHSSFCellStyle;
                //  ICell cell4 = row3.CreateCell(4);
                //  cell4.SetCellValue("数量(台)");
                //  sheet.SetColumnWidth(5, 5000);
                // // cell4.CellStyle = mHSSFCellStyle;
                //  ICell cell5 = row3.CreateCell(5);
                //  cell5.SetCellValue("合同额");
                //  //cell5.CellStyle = mHSSFCellStyle;
                //  ICell cell6 = row3.CreateCell(6);
                //  sheet.SetColumnWidth(6, 5000);
                //  cell6.SetCellValue("成本");
                // // cell6.CellStyle = mHSSFCellStyle;
                //  sheet.SetColumnWidth(7, 5000);
                //  ICell cell7 = row3.CreateCell(7);
                //  cell7.SetCellValue("毛利润");
                ////  cell7.CellStyle = mHSSFCellStyle;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 2, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 7));


                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 4);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                //

                IRow bottomrow = sheet.CreateRow(dt.Rows.Count + 4);//在工作表中添加一行
                IRow bottomrow1 = sheet.CreateRow(dt.Rows.Count + 5);
                ICell bottomcell = bottomrow.CreateCell(6);//在行中添加一列
                ICell bottomcell1 = bottomrow1.CreateCell(6);
                bottomcell.SetCellValue("燕山输配产品部");
                bottomcell1.SetCellValue("截止日期：" + End);
                //bottomcell.CellStyle = mHSSFCellStyle;
                // bottomcell1.CellStyle = mHSSFCellStyle;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 6, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 5, dt.Rows.Count + 5, 6, 7));
                //

                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }

        /// <summary>
        /// 调压箱
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="a_strTitle"></param>
        /// <param name="Start"></param>
        /// <param name="a_strTols"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public static MemoryStream PressureRegulatingBoxToExcel(DataTable dt, string a_strTitle, string Start, string[] a_strTols, string StartDate ,string EndDate)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 15));
                cellTitle.CellStyle = mHSSFCellStyle;
                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(14);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;
                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("常规/技改");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("规则型号");
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("单位");

                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue(Start + "至" + EndDate);
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(6);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("年度累计");
                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(13);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("产品状态");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 0, 0));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 6, 12));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 13, 14));
                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                NPOI.SS.UserModel.ICell R3cell1 = row3.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R3cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell1.SetCellValue("销售数量");
                NPOI.SS.UserModel.ICell R3cell2 = row3.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                R3cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell2.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell R3cell3 = row3.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                R3cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell3.SetCellValue("数量");
                NPOI.SS.UserModel.ICell R3cell4 = row3.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                R3cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell4.SetCellValue("销售均价");
                NPOI.SS.UserModel.ICell R3cell5 = row3.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                R3cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell5.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell R3cell6 = row3.CreateCell(9);
                sheet.SetColumnWidth(9, Convert.ToInt32(5000));
                R3cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell6.SetCellValue("单位成本");
                NPOI.SS.UserModel.ICell R3cell7 = row3.CreateCell(10);
                sheet.SetColumnWidth(10, Convert.ToInt32(5000));
                R3cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell7.SetCellValue("累计直接成本");
                NPOI.SS.UserModel.ICell R3cell8 = row3.CreateCell(11);
                sheet.SetColumnWidth(11, Convert.ToInt32(5000));
                R3cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell8.SetCellValue("毛利润");
                NPOI.SS.UserModel.ICell R3cell9 = row3.CreateCell(12);
                sheet.SetColumnWidth(12, Convert.ToInt32(5000));
                R3cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell9.SetCellValue("直接成本占销售比例");

                NPOI.SS.UserModel.ICell R3cell10 = row3.CreateCell(13);
                sheet.SetColumnWidth(13, Convert.ToInt32(5000));
                R3cell10.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell10.SetCellValue("已发货数量");
                NPOI.SS.UserModel.ICell R3cell11 = row3.CreateCell(14);
                sheet.SetColumnWidth(14, Convert.ToInt32(5000));
                R3cell11.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell11.SetCellValue("未发货数量");
                //NPOI.SS.UserModel.ICell R3cell12 = row3.CreateCell(15);
                //sheet.SetColumnWidth(15, Convert.ToInt32(5000));
                //R3cell12.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //R3cell12.SetCellValue("生产中数量");




                int count2 = 2;
                //for (int i = 0; i < a_strTols.Length; i++)//生成sheet第一行列名  
                //{
                //    sheet.SetColumnWidth(i, Convert.ToInt32(a_strTols[i].Split('@')[1]));
                //    NPOI.SS.UserModel.ICell cell = row3.CreateCell(count2++);
                //    cell.SetCellValue(a_strTols[i].Split('@')[0]);
                //    // cell.CellStyle = CellStyleContent(workbook);
                //    // cell.CellStyle = mHSSFCellStyle;
                //}





                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 4);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                //

                IRow bottomrow = sheet.CreateRow(dt.Rows.Count + 4);//在工作表中添加一行
                IRow bottomrow1 = sheet.CreateRow(dt.Rows.Count + 5);
                ICell bottomcell = bottomrow.CreateCell(14);//在行中添加一列
                ICell bottomcell1 = bottomrow1.CreateCell(14);
                bottomcell.SetCellValue("燕山输配产品部");
                bottomcell1.SetCellValue("截止日期：" + StartDate+"-"+EndDate);
                //bottomcell.CellStyle = mHSSFCellStyle;
                // bottomcell1.CellStyle = mHSSFCellStyle;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 6, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 5, dt.Rows.Count + 5, 6, 7));
                //

                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }

        /// <summary>
        /// 高压箱
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="a_strTitle"></param>
        /// <param name="Start"></param>
        /// <param name="a_strTols"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public static MemoryStream HighVoltageCompartmentToExcel(DataTable dt, string a_strTitle, string Start, string[] a_strTols,  string StartDate ,string EndDate)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 15));
                cellTitle.CellStyle = mHSSFCellStyle;
                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(14);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;
                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("名称");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("规则型号");
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("单位");

                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue(Start + "至" + EndDate);
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(6);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("年度累计");
                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(13);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("产品状态");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 0, 0));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 6, 12));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 13, 14));
                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                NPOI.SS.UserModel.ICell R3cell1 = row3.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R3cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell1.SetCellValue("销售数量");
                NPOI.SS.UserModel.ICell R3cell2 = row3.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                R3cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell2.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell R3cell3 = row3.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                R3cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell3.SetCellValue("数量");
                NPOI.SS.UserModel.ICell R3cell4 = row3.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                R3cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell4.SetCellValue("销售均价");
                NPOI.SS.UserModel.ICell R3cell5 = row3.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                R3cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell5.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell R3cell6 = row3.CreateCell(9);
                sheet.SetColumnWidth(9, Convert.ToInt32(5000));
                R3cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell6.SetCellValue("单位成本");
                NPOI.SS.UserModel.ICell R3cell7 = row3.CreateCell(10);
                sheet.SetColumnWidth(10, Convert.ToInt32(5000));
                R3cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell7.SetCellValue("累计直接成本");
                NPOI.SS.UserModel.ICell R3cell8 = row3.CreateCell(11);
                sheet.SetColumnWidth(11, Convert.ToInt32(5000));
                R3cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell8.SetCellValue("毛利润");
                NPOI.SS.UserModel.ICell R3cell9 = row3.CreateCell(12);
                sheet.SetColumnWidth(12, Convert.ToInt32(5000));
                R3cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell9.SetCellValue("直接成本占销售比例");

                NPOI.SS.UserModel.ICell R3cell10 = row3.CreateCell(13);
                sheet.SetColumnWidth(13, Convert.ToInt32(5000));
                R3cell10.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell10.SetCellValue("已发货数量");
                NPOI.SS.UserModel.ICell R3cell11 = row3.CreateCell(14);
                sheet.SetColumnWidth(14, Convert.ToInt32(5000));
                R3cell11.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell11.SetCellValue("未发货数量");
                //NPOI.SS.UserModel.ICell R3cell12 = row3.CreateCell(15);
                //sheet.SetColumnWidth(15, Convert.ToInt32(5000));
                //R3cell12.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //R3cell12.SetCellValue("生产中数量");




                int count2 = 2;
                //for (int i = 0; i < a_strTols.Length; i++)//生成sheet第一行列名  
                //{
                //    sheet.SetColumnWidth(i, Convert.ToInt32(a_strTols[i].Split('@')[1]));
                //    NPOI.SS.UserModel.ICell cell = row3.CreateCell(count2++);
                //    cell.SetCellValue(a_strTols[i].Split('@')[0]);
                //    // cell.CellStyle = CellStyleContent(workbook);
                //    // cell.CellStyle = mHSSFCellStyle;
                //}





                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 4);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                //

                IRow bottomrow = sheet.CreateRow(dt.Rows.Count + 4);//在工作表中添加一行
                IRow bottomrow1 = sheet.CreateRow(dt.Rows.Count + 5);
                ICell bottomcell = bottomrow.CreateCell(14);//在行中添加一列
                ICell bottomcell1 = bottomrow1.CreateCell(14);
                bottomcell.SetCellValue("燕山输配产品部");
                bottomcell1.SetCellValue("截止日期：" + StartDate+"-"+EndDate);
                //bottomcell.CellStyle = mHSSFCellStyle;
                // bottomcell1.CellStyle = mHSSFCellStyle;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 6, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 5, dt.Rows.Count + 5, 6, 7));
                //

                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }
        /// <summary>
        /// 切断阀
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="a_strTitle"></param>
        /// <param name="Start"></param>
        /// <param name="a_strTols"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public static MemoryStream CutOffSalesSummaryToExcel(DataTable dt, string a_strTitle, string Start, string[] a_strTols, string StartDate ,string EndDate)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 15));
                cellTitle.CellStyle = mHSSFCellStyle;
                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(14);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;
                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("规则型号");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("单位");
                //NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                //sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                //cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //cell4.SetCellValue("单位");

                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue(Start + "至" + EndDate);
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("年度累计");
                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(12);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("产品状态");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 0, 0));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 3, 4));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 5, 12));
                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 6, 12));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 13, 14));
                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                NPOI.SS.UserModel.ICell R3cell1 = row3.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                R3cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell1.SetCellValue("销售数量");
                NPOI.SS.UserModel.ICell R3cell2 = row3.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R3cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell2.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell R3cell3 = row3.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                R3cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell3.SetCellValue("数量");
                NPOI.SS.UserModel.ICell R3cell4 = row3.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                R3cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell4.SetCellValue("销售均价");
                NPOI.SS.UserModel.ICell R3cell5 = row3.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                R3cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell5.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell R3cell6 = row3.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                R3cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell6.SetCellValue("更换线圈后直接成本");


                NPOI.SS.UserModel.ICell R3cell7 = row3.CreateCell(9);
                sheet.SetColumnWidth(9, Convert.ToInt32(5000));
                R3cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell7.SetCellValue("C-C型直接成本");
                NPOI.SS.UserModel.ICell R3cell8 = row3.CreateCell(10);
                sheet.SetColumnWidth(10, Convert.ToInt32(5000));
                R3cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell8.SetCellValue("累计直接成本");
                NPOI.SS.UserModel.ICell R3cell9 = row3.CreateCell(11);
                sheet.SetColumnWidth(11, Convert.ToInt32(5000));
                R3cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell9.SetCellValue("毛利润");
                NPOI.SS.UserModel.ICell R3cell10 = row3.CreateCell(12);
                sheet.SetColumnWidth(12, Convert.ToInt32(5000));
                R3cell10.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell10.SetCellValue("直接成本占销售比例");

                NPOI.SS.UserModel.ICell R3cell11 = row3.CreateCell(13);
                sheet.SetColumnWidth(13, Convert.ToInt32(5000));
                R3cell11.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell11.SetCellValue("已发货数量");
                NPOI.SS.UserModel.ICell R3cell12 = row3.CreateCell(14);
                sheet.SetColumnWidth(14, Convert.ToInt32(5000));
                R3cell12.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell12.SetCellValue("未发货数量");
                //NPOI.SS.UserModel.ICell R3cell12 = row3.CreateCell(15);
                //sheet.SetColumnWidth(15, Convert.ToInt32(5000));
                //R3cell12.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //R3cell12.SetCellValue("生产中数量");




                int count2 = 2;
                //for (int i = 0; i < a_strTols.Length; i++)//生成sheet第一行列名  
                //{
                //    sheet.SetColumnWidth(i, Convert.ToInt32(a_strTols[i].Split('@')[1]));
                //    NPOI.SS.UserModel.ICell cell = row3.CreateCell(count2++);
                //    cell.SetCellValue(a_strTols[i].Split('@')[0]);
                //    // cell.CellStyle = CellStyleContent(workbook);
                //    // cell.CellStyle = mHSSFCellStyle;
                //}





                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 4);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                //

                IRow bottomrow = sheet.CreateRow(dt.Rows.Count + 4);//在工作表中添加一行
                IRow bottomrow1 = sheet.CreateRow(dt.Rows.Count + 5);
                ICell bottomcell = bottomrow.CreateCell(14);//在行中添加一列
                ICell bottomcell1 = bottomrow1.CreateCell(14);
                bottomcell.SetCellValue("燕山输配产品部");
                bottomcell1.SetCellValue("截止日期：" + StartDate+"-"+EndDate);
                //bottomcell.CellStyle = mHSSFCellStyle;
                // bottomcell1.CellStyle = mHSSFCellStyle;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 6, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 5, dt.Rows.Count + 5, 6, 7));
                //

                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }
        /// <summary>
        /// 其他设备
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="a_strTitle"></param>
        /// <param name="Start"></param>
        /// <param name="a_strTols"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public static MemoryStream OtherEquipmentTableToExcel(DataTable dt, string a_strTitle, string Start, string[] a_strTols,  string StartDate ,string EndDate)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象  
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN; // HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN; // BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;   //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框
                mHSSFCellStyle.WrapText = false;

                ICellStyle cellStyleDate = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");
                IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行
                ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列
                cellTitle.SetCellValue(a_strTitle);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 15));
                cellTitle.CellStyle = mHSSFCellStyle;
                IRow toprow = sheet.CreateRow(1);
                ICell topcelll = toprow.CreateCell(14);
                topcelll.SetCellValue("单位：万元");
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;

                cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                topcelll.CellStyle.Alignment = HorizontalAlignment.RIGHT;
                //使用NPOI操作Excel表  
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(2);
                int count = 0;
                NPOI.SS.UserModel.ICell cell1 = row.CreateCell(0);
                sheet.SetColumnWidth(0, Convert.ToInt32(5000));
                cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell1.SetCellValue("序号");
                NPOI.SS.UserModel.ICell cell2 = row.CreateCell(1);
                sheet.SetColumnWidth(1, Convert.ToInt32(5000));
                cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell2.SetCellValue("名称");
                NPOI.SS.UserModel.ICell cell3 = row.CreateCell(2);
                sheet.SetColumnWidth(2, Convert.ToInt32(5000));
                cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell3.SetCellValue("规则型号");
                NPOI.SS.UserModel.ICell cell4 = row.CreateCell(3);
                sheet.SetColumnWidth(3, Convert.ToInt32(5000));
                cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell4.SetCellValue("单位");

                NPOI.SS.UserModel.ICell cell5 = row.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell5.SetCellValue(Start + "至" + EndDate);
                NPOI.SS.UserModel.ICell cell6 = row.CreateCell(6);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell6.SetCellValue("年度累计");
                NPOI.SS.UserModel.ICell cell7 = row.CreateCell(13);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                cell7.SetCellValue("产品状态");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 0, 0));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 1, 1));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 2, 2));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 3, 3, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 4, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 6, 12));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 13, 14));
                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
                NPOI.SS.UserModel.ICell R3cell1 = row3.CreateCell(4);
                sheet.SetColumnWidth(4, Convert.ToInt32(5000));
                R3cell1.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell1.SetCellValue("销售数量");
                NPOI.SS.UserModel.ICell R3cell2 = row3.CreateCell(5);
                sheet.SetColumnWidth(5, Convert.ToInt32(5000));
                R3cell2.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell2.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell R3cell3 = row3.CreateCell(6);
                sheet.SetColumnWidth(6, Convert.ToInt32(5000));
                R3cell3.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell3.SetCellValue("数量");
                NPOI.SS.UserModel.ICell R3cell4 = row3.CreateCell(7);
                sheet.SetColumnWidth(7, Convert.ToInt32(5000));
                R3cell4.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell4.SetCellValue("销售均价");
                NPOI.SS.UserModel.ICell R3cell5 = row3.CreateCell(8);
                sheet.SetColumnWidth(8, Convert.ToInt32(5000));
                R3cell5.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell5.SetCellValue("合同额");
                NPOI.SS.UserModel.ICell R3cell6 = row3.CreateCell(9);
                sheet.SetColumnWidth(9, Convert.ToInt32(5000));
                R3cell6.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell6.SetCellValue("单位成本");
                NPOI.SS.UserModel.ICell R3cell7 = row3.CreateCell(10);
                sheet.SetColumnWidth(10, Convert.ToInt32(5000));
                R3cell7.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell7.SetCellValue("累计直接成本");
                NPOI.SS.UserModel.ICell R3cell8 = row3.CreateCell(11);
                sheet.SetColumnWidth(11, Convert.ToInt32(5000));
                R3cell8.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell8.SetCellValue("毛利润");
                NPOI.SS.UserModel.ICell R3cell9 = row3.CreateCell(12);
                sheet.SetColumnWidth(12, Convert.ToInt32(5000));
                R3cell9.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell9.SetCellValue("直接成本占销售比例");

                NPOI.SS.UserModel.ICell R3cell10 = row3.CreateCell(13);
                sheet.SetColumnWidth(13, Convert.ToInt32(5000));
                R3cell10.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell10.SetCellValue("已发货数量");
                NPOI.SS.UserModel.ICell R3cell11 = row3.CreateCell(14);
                sheet.SetColumnWidth(14, Convert.ToInt32(5000));
                R3cell11.CellStyle.Alignment = HorizontalAlignment.CENTER;
                R3cell11.SetCellValue("未发货数量");
                //NPOI.SS.UserModel.ICell R3cell12 = row3.CreateCell(15);
                //sheet.SetColumnWidth(15, Convert.ToInt32(5000));
                //R3cell12.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //R3cell12.SetCellValue("生产中数量");




                int count2 = 2;
                //for (int i = 0; i < a_strTols.Length; i++)//生成sheet第一行列名  
                //{
                //    sheet.SetColumnWidth(i, Convert.ToInt32(a_strTols[i].Split('@')[1]));
                //    NPOI.SS.UserModel.ICell cell = row3.CreateCell(count2++);
                //    cell.SetCellValue(a_strTols[i].Split('@')[0]);
                //    // cell.CellStyle = CellStyleContent(workbook);
                //    // cell.CellStyle = mHSSFCellStyle;
                //}





                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 4);
                    count = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][j].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue((int)dt.Rows[i][j]);
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue((Double)dt.Rows[i][j]);
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue((bool)dt.Rows[i][j]);
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][j].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        // cell.CellStyle = mHSSFCellStyle;
                    }
                }

                //

                IRow bottomrow = sheet.CreateRow(dt.Rows.Count + 4);//在工作表中添加一行
                IRow bottomrow1 = sheet.CreateRow(dt.Rows.Count + 5);
                ICell bottomcell = bottomrow.CreateCell(14);//在行中添加一列
                ICell bottomcell1 = bottomrow1.CreateCell(14);
                bottomcell.SetCellValue("燕山输配产品部");
                bottomcell1.SetCellValue("截止日期：" + StartDate+"-"+EndDate);
                //bottomcell.CellStyle = mHSSFCellStyle;
                // bottomcell1.CellStyle = mHSSFCellStyle;
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 4, dt.Rows.Count + 4, 6, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(dt.Rows.Count + 5, dt.Rows.Count + 5, 6, 7));
                //

                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }

        public static MemoryStream ExportDataTableToExcelServicing(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                IRow header2Row = sheet.CreateRow(1);
                IRow header3Row = sheet.CreateRow(2);

                headerRow.Height = 700;
                header2Row.Height = 350;
                header3Row.Height = 350;


                sheet.SetColumnWidth(0, 7 * 256);
                sheet.SetColumnWidth(1, 10 * 256);
                sheet.SetColumnWidth(2, 11 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 25 * 256);
                sheet.SetColumnWidth(5, 25 * 256);
                sheet.SetColumnWidth(6, 15 * 256);
                sheet.SetColumnWidth(7, 15 * 256);
                sheet.SetColumnWidth(8, 20 * 256);
                sheet.SetColumnWidth(9, 20 * 256);
                sheet.SetColumnWidth(10, 20 * 256);
                sheet.SetColumnWidth(11, 20 * 256);
                sheet.SetColumnWidth(12, 20 * 256);

                ICell Icell = headerRow.CreateCell(0);
                Icell.CellStyle = SetCellStyle2(workbook);
                Icell.SetCellValue("零备件费用");
                ICell Icell1 = headerRow.CreateCell(1);
                Icell1.CellStyle = SetCellStyle2(workbook);
                Icell1.SetCellValue("");
                ICell Icell2 = headerRow.CreateCell(2);
                Icell2.CellStyle = SetCellStyle2(workbook);
                Icell2.SetCellValue("");
                ICell Icell3 = headerRow.CreateCell(3);
                Icell3.CellStyle = SetCellStyle2(workbook);
                Icell3.SetCellValue("");
                ICell Icell4 = headerRow.CreateCell(4);
                Icell4.CellStyle = SetCellStyle2(workbook);
                Icell4.SetCellValue("");
                ICell Icell5 = headerRow.CreateCell(5);
                Icell5.CellStyle = SetCellStyle2(workbook);
                Icell5.SetCellValue("");
                ICell Icell6 = headerRow.CreateCell(6);
                Icell6.CellStyle = SetCellStyle2(workbook);
                Icell6.SetCellValue("");
                ICell Icell7 = headerRow.CreateCell(7);
                Icell7.CellStyle = SetCellStyle2(workbook);
                Icell7.SetCellValue("");
                ICell Icell8 = headerRow.CreateCell(8);
                Icell8.CellStyle = SetCellStyle2(workbook);
                Icell8.SetCellValue("");
                ICell Icell9 = headerRow.CreateCell(9);
                Icell9.CellStyle = SetCellStyle2(workbook);
                Icell9.SetCellValue("");

                ICell Icell10 = headerRow.CreateCell(10);
                Icell10.CellStyle = SetCellStyle2(workbook);
                Icell10.SetCellValue("");
                ICell Icell11 = headerRow.CreateCell(11);
                Icell11.CellStyle = SetCellStyle2(workbook);
                Icell11.SetCellValue("");

                ICell Icell12 = headerRow.CreateCell(12);
                Icell12.CellStyle = SetCellStyle2(workbook);
                Icell12.SetCellValue("");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 12));

                ICell I2cell = header2Row.CreateCell(0);
                I2cell.CellStyle = SetCellStyle2(workbook);
                I2cell.SetCellValue("序号");
                ICell I2cell1 = header2Row.CreateCell(1);
                I2cell1.CellStyle = SetCellStyle2(workbook);
                I2cell1.SetCellValue("项目");
                ICell I2cell2 = header2Row.CreateCell(2);
                I2cell2.CellStyle = SetCellStyle2(workbook);
                I2cell2.SetCellValue("");
                ICell I2cell3 = header2Row.CreateCell(3);
                I2cell3.CellStyle = SetCellStyle2(workbook);
                I2cell3.SetCellValue("");
                ICell I2cell4 = header2Row.CreateCell(4);
                I2cell4.CellStyle = SetCellStyle2(workbook);
                I2cell4.SetCellValue("");
                ICell I2cell5 = header2Row.CreateCell(5);
                I2cell5.CellStyle = SetCellStyle2(workbook);
                I2cell5.SetCellValue("单价（元）");
                ICell I2cell6 = header2Row.CreateCell(6);
                I2cell6.CellStyle = SetCellStyle2(workbook);
                I2cell6.SetCellValue("数量");
                ICell I2cell7 = header2Row.CreateCell(7);
                I2cell7.CellStyle = SetCellStyle2(workbook);
                I2cell7.SetCellValue("单位");
                ICell I2cell8 = header2Row.CreateCell(8);
                I2cell8.CellStyle = SetCellStyle2(workbook);
                I2cell8.SetCellValue("金额（元）");
                ICell I2cell9 = header2Row.CreateCell(9);
                I2cell9.CellStyle = SetCellStyle2(workbook);
                I2cell9.SetCellValue("清洗保养");


                ICell I2cell10 = header2Row.CreateCell(10);
                I2cell10.CellStyle = SetCellStyle2(workbook);
                I2cell10.SetCellValue("出厂标定");
                ICell I2cell11 = header2Row.CreateCell(11);
                I2cell11.CellStyle = SetCellStyle2(workbook);
                I2cell11.SetCellValue("气密性检测");

                ICell I2cell12 = header2Row.CreateCell(12);
                I2cell12.CellStyle = SetCellStyle2(workbook);
                I2cell12.SetCellValue("合计（元）");

                ICell I3cell = header3Row.CreateCell(0);
                I3cell.CellStyle = SetCellStyle2(workbook);
                I3cell.SetCellValue("");
                ICell I3cell1 = header3Row.CreateCell(1);
                I3cell1.CellStyle = SetCellStyle2(workbook);
                I3cell1.SetCellValue("规格型号");
                ICell I3cell2 = header3Row.CreateCell(2);
                I3cell2.CellStyle = SetCellStyle2(workbook);
                I3cell2.SetCellValue("口径");
                ICell I3cell3 = header3Row.CreateCell(3);
                I3cell3.CellStyle = SetCellStyle2(workbook);
                I3cell3.SetCellValue("地点");
                ICell I3cell4 = header3Row.CreateCell(4);
                I3cell4.CellStyle = SetCellStyle2(workbook);
                I3cell4.SetCellValue("修理内容");
                ICell I3cell5 = header3Row.CreateCell(5);
                I3cell5.CellStyle = SetCellStyle2(workbook);
                I3cell5.SetCellValue("");
                ICell I3cell6 = header3Row.CreateCell(6);
                I3cell6.CellStyle = SetCellStyle2(workbook);
                I3cell6.SetCellValue("");
                ICell I3cell7 = header3Row.CreateCell(7);
                I3cell7.CellStyle = SetCellStyle2(workbook);
                I3cell7.SetCellValue("");
                ICell I3cell8 = header3Row.CreateCell(8);
                I3cell8.CellStyle = SetCellStyle2(workbook);
                I3cell8.SetCellValue("");
                ICell I3cell9 = header3Row.CreateCell(9);
                I3cell9.CellStyle = SetCellStyle2(workbook);
                I3cell9.SetCellValue("");
                ICell I3cell10 = header3Row.CreateCell(10);
                I3cell10.CellStyle = SetCellStyle2(workbook);
                I3cell10.SetCellValue("");
                ICell I3cell11 = header3Row.CreateCell(11);
                I3cell11.CellStyle = SetCellStyle2(workbook);
                I3cell11.SetCellValue("");

                ICell I3cell12 = header3Row.CreateCell(12);
                I3cell12.CellStyle = SetCellStyle2(workbook);
                I3cell12.SetCellValue("");


                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 1, 4));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));



                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 5, 5));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 6, 6));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 7, 7));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 8, 8));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 9, 9));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 10, 10));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 11, 11));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 12, 12));
                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色
                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色
                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色
                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充



                //    //set date format  
                //ICellStyle cellStyleDate = workbook.CreateCellStyle();
                //IDataFormat format = workbook.CreateDataFormat();
                //cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");

                //IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行

                //ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列


                //cellTitle.SetCellValue(a_strTitle);

                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, a_strCols.Length - 1));

                //cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                //cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //使用NPOI操作Excel表  
                //NPOI.SS.UserModel.IRow row = sheet.CreateRow(1);
                int count = 0;
                //for (int i = 0; i < a_strCols.Length; i++)//生成sheet第一行列名  
                //{
                //    sheet.SetColumnWidth(i, Convert.ToInt32(a_strCols[i].Split('-')[1]));
                //    NPOI.SS.UserModel.ICell cell = row.CreateCell(count++);
                //    cell.SetCellValue(a_strCols[i].Split('-')[0]);
                //    //cell.CellStyle = CellStyleContent(workbook);
                //    cell.CellStyle = mHSSFCellStyle;
                //}
                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 3);
                    count = 0;
                    for (int j = 0; j < 13; j++)
                    {
                        var s = a_strCols[j];
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);


                        Type type = typeof(string);
                        try
                        {
                            type = dt.Rows[i][s].GetType();
                        }
                        catch (Exception)
                        {
                            cell.SetCellValue("无");
                            cell.CellStyle = mHSSFCellStyle;
                            continue;
                        }
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {
                            cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue((Convert.ToDateTime(dt.Rows[i][s])).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][s].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }


        public static MemoryStream ExportDataTableToExcelSchedule(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");





                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充
                int count = 0;
                NPOI.SS.UserModel.IRow rows0 = sheet.CreateRow(0);
                for (int i = 0; i < a_strCols.Count(); i++)
                {

                    NPOI.SS.UserModel.ICell cell = rows0.CreateCell(count++);
                    if (i == 0)
                    {
                        cell.SetCellValue(a_strTitle);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                    else
                    {
                        cell.SetCellValue("");
                        cell.CellStyle = mHSSFCellStyle;
                    }

                }
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, a_strCols.Count() - 1));
                count = 0;
                NPOI.SS.UserModel.IRow rows1 = sheet.CreateRow(1);
                for (int i = 0; i < a_strCols.Count(); i++)
                {

                    NPOI.SS.UserModel.ICell cell = rows1.CreateCell(count++);
                    cell.SetCellValue(a_strCols[i]);
                    cell.CellStyle = mHSSFCellStyle;
                }


                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 2);
                    count = 0;
                    for (int j = 0; j < a_strCols.Count(); j++)
                    {
                        var s = a_strCols[j];
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][s].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {

                            cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(Convert.ToDateTime(dt.Rows[i][s]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][s].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }

        public static MemoryStream ExportDataTableToExcelCheckData(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                IRow header2Row = sheet.CreateRow(1);


                headerRow.Height = 700;
                header2Row.Height = 350;



                //sheet.SetColumnWidth(0, 7 * 256);
                //sheet.SetColumnWidth(1, 10 * 256);
                //sheet.SetColumnWidth(2, 11 * 256);
                //sheet.SetColumnWidth(3, 20 * 256);
                //sheet.SetColumnWidth(4, 25 * 256);
                //sheet.SetColumnWidth(5, 25 * 256);
                //sheet.SetColumnWidth(6, 15 * 256);
                //sheet.SetColumnWidth(7, 15 * 256);
                //sheet.SetColumnWidth(8, 20 * 256);
                //sheet.SetColumnWidth(9, 20 * 256);
                //sheet.SetColumnWidth(10, 20 * 256);
                //sheet.SetColumnWidth(11, 20 * 256);
                //sheet.SetColumnWidth(12, 20 * 256);

                ICell Icell = headerRow.CreateCell(0);
                Icell.CellStyle = SetCellStyle2(workbook);
                Icell.SetCellValue("");
                ICell Icell1 = headerRow.CreateCell(1);
                Icell1.CellStyle = SetCellStyle2(workbook);
                Icell1.SetCellValue("");
                ICell Icell2 = headerRow.CreateCell(2);
                Icell2.CellStyle = SetCellStyle2(workbook);
                Icell2.SetCellValue("");
                ICell Icell3 = headerRow.CreateCell(3);
                Icell3.CellStyle = SetCellStyle2(workbook);
                Icell3.SetCellValue("");
                ICell Icell4 = headerRow.CreateCell(4);
                Icell4.CellStyle = SetCellStyle2(workbook);
                Icell4.SetCellValue("流量点m3/h");
                ICell Icell5 = headerRow.CreateCell(5);
                Icell5.CellStyle = SetCellStyle2(workbook);
                Icell5.SetCellValue("");
                ICell Icell6 = headerRow.CreateCell(6);
                Icell6.CellStyle = SetCellStyle2(workbook);
                Icell6.SetCellValue("");
                ICell Icell7 = headerRow.CreateCell(7);
                Icell7.CellStyle = SetCellStyle2(workbook);
                Icell7.SetCellValue("");
                ICell Icell8 = headerRow.CreateCell(8);
                Icell8.CellStyle = SetCellStyle2(workbook);
                Icell8.SetCellValue("");
                ICell Icell9 = headerRow.CreateCell(9);
                Icell9.CellStyle = SetCellStyle2(workbook);
                Icell9.SetCellValue("");

                ICell Icell10 = headerRow.CreateCell(10);
                Icell10.CellStyle = SetCellStyle2(workbook);
                Icell10.SetCellValue("");
                ICell Icell11 = headerRow.CreateCell(11);
                Icell11.CellStyle = SetCellStyle2(workbook);
                Icell11.SetCellValue("平均仪表系数  1/m3");

                ICell Icell12 = headerRow.CreateCell(12);
                Icell12.CellStyle = SetCellStyle2(workbook);
                Icell12.SetCellValue("");

                ICell Icell13 = headerRow.CreateCell(13);
                Icell13.CellStyle = SetCellStyle2(workbook);
                Icell13.SetCellValue("");

                ICell Icell14 = headerRow.CreateCell(14);
                Icell14.CellStyle = SetCellStyle2(workbook);
                Icell14.SetCellValue("");

                ICell Icell15 = headerRow.CreateCell(15);
                Icell15.CellStyle = SetCellStyle2(workbook);
                Icell15.SetCellValue("");

                ICell Icell16 = headerRow.CreateCell(16);
                Icell16.CellStyle = SetCellStyle2(workbook);
                Icell16.SetCellValue("");

                ICell Icell17 = headerRow.CreateCell(17);
                Icell17.CellStyle = SetCellStyle2(workbook);
                Icell17.SetCellValue("");


                ICell Icell18 = headerRow.CreateCell(18);
                Icell18.CellStyle = SetCellStyle2(workbook);
                Icell18.SetCellValue("重复性 %");

                ICell Icell19 = headerRow.CreateCell(19);
                Icell19.CellStyle = SetCellStyle2(workbook);
                Icell19.SetCellValue("");

                ICell Icell20 = headerRow.CreateCell(20);
                Icell20.CellStyle = SetCellStyle2(workbook);
                Icell20.SetCellValue("");

                ICell Icell21 = headerRow.CreateCell(21);
                Icell21.CellStyle = SetCellStyle2(workbook);
                Icell21.SetCellValue("");

                ICell Icell22 = headerRow.CreateCell(22);
                Icell22.CellStyle = SetCellStyle2(workbook);
                Icell22.SetCellValue("");

                ICell Icell23 = headerRow.CreateCell(23);
                Icell23.CellStyle = SetCellStyle2(workbook);
                Icell23.SetCellValue("");

                ICell Icell24 = headerRow.CreateCell(24);
                Icell24.CellStyle = SetCellStyle2(workbook);
                Icell24.SetCellValue("");

                ICell Icell25 = headerRow.CreateCell(25);
                Icell25.CellStyle = SetCellStyle2(workbook);
                Icell25.SetCellValue("");

                ICell Icell26 = headerRow.CreateCell(26);
                Icell26.CellStyle = SetCellStyle2(workbook);
                Icell26.SetCellValue("仪表系数(1/m3)");

                ICell Icell27 = headerRow.CreateCell(27);
                Icell27.CellStyle = SetCellStyle2(workbook);
                Icell27.SetCellValue("高频 示值误差(%)");

                ICell Icell28 = headerRow.CreateCell(28);
                Icell28.CellStyle = SetCellStyle2(workbook);
                Icell28.SetCellValue("拍照示值误差(%)");

                ICell Icell29 = headerRow.CreateCell(29);
                Icell29.CellStyle = SetCellStyle2(workbook);
                Icell29.SetCellValue("原仪表系数");


                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 1, 2));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 4, 10));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 11, 17));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 18, 24));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 26, 27));
                ICell I2cell = header2Row.CreateCell(0);
                I2cell.CellStyle = SetCellStyle2(workbook);
                I2cell.SetCellValue("");
                ICell I2cell1 = header2Row.CreateCell(1);
                I2cell1.CellStyle = SetCellStyle2(workbook);
                I2cell1.SetCellValue("出厂编号");
                ICell I2cell2 = header2Row.CreateCell(2);
                I2cell2.CellStyle = SetCellStyle2(workbook);
                I2cell2.SetCellValue("证书编号");
                ICell I2cell3 = header2Row.CreateCell(3);
                I2cell3.CellStyle = SetCellStyle2(workbook);
                I2cell3.SetCellValue("测试结果备注");
                ICell I2cell4 = header2Row.CreateCell(4);
                I2cell4.CellStyle = SetCellStyle2(workbook);
                I2cell4.SetCellValue("Qmin");
                ICell I2cell5 = header2Row.CreateCell(5);
                I2cell5.CellStyle = SetCellStyle2(workbook);
                I2cell5.SetCellValue("0.1Qmax");
                ICell I2cell6 = header2Row.CreateCell(6);
                I2cell6.CellStyle = SetCellStyle2(workbook);
                I2cell6.SetCellValue("0.2Qmax");
                ICell I2cell7 = header2Row.CreateCell(7);
                I2cell7.CellStyle = SetCellStyle2(workbook);
                I2cell7.SetCellValue("0.25Qmax");
                ICell I2cell8 = header2Row.CreateCell(8);
                I2cell8.CellStyle = SetCellStyle2(workbook);
                I2cell8.SetCellValue("0.4Qmax");
                ICell I2cell9 = header2Row.CreateCell(9);
                I2cell9.CellStyle = SetCellStyle2(workbook);
                I2cell9.SetCellValue("0.7Qmax");


                ICell I2cell10 = header2Row.CreateCell(10);
                I2cell10.CellStyle = SetCellStyle2(workbook);
                I2cell10.SetCellValue("Qmax");
                ICell I2cell11 = header2Row.CreateCell(11);
                I2cell11.CellStyle = SetCellStyle2(workbook);
                I2cell11.SetCellValue("Qmin");

                ICell I2cell12 = header2Row.CreateCell(12);
                I2cell12.CellStyle = SetCellStyle2(workbook);
                I2cell12.SetCellValue("0.1Qmax");

                ICell I2cell13 = header2Row.CreateCell(13);
                I2cell13.CellStyle = SetCellStyle2(workbook);
                I2cell13.SetCellValue("0.1Qmax");
                ICell I2cell14 = header2Row.CreateCell(14);
                I2cell14.CellStyle = SetCellStyle2(workbook);
                I2cell14.SetCellValue("0.2Qmax");
                ICell I2cell15 = header2Row.CreateCell(15);
                I2cell15.CellStyle = SetCellStyle2(workbook);
                I2cell15.SetCellValue("0.25Qmax");
                ICell I2cell16 = header2Row.CreateCell(16);
                I2cell16.CellStyle = SetCellStyle2(workbook);
                I2cell16.SetCellValue("0.4Qmax");
                ICell I2cell17 = header2Row.CreateCell(17);
                I2cell17.CellStyle = SetCellStyle2(workbook);
                I2cell17.SetCellValue("0.7Qmax");
                ICell I2cell18 = header2Row.CreateCell(18);
                I2cell18.CellStyle = SetCellStyle2(workbook);
                I2cell18.SetCellValue("Qmax");
                ICell I2cell19 = header2Row.CreateCell(19);
                I2cell19.CellStyle = SetCellStyle2(workbook);
                I2cell19.SetCellValue("Qmin");
                ICell I2cell20 = header2Row.CreateCell(20);
                I2cell20.CellStyle = SetCellStyle2(workbook);
                I2cell20.SetCellValue("0.1Qmax");
                ICell I2cell21 = header2Row.CreateCell(21);
                I2cell21.CellStyle = SetCellStyle2(workbook);
                I2cell21.SetCellValue("0.25Qmax");
                ICell I2cell22 = header2Row.CreateCell(22);
                I2cell22.CellStyle = SetCellStyle2(workbook);
                I2cell22.SetCellValue("0.4Qmax");


                ICell I2cell23 = header2Row.CreateCell(23);
                I2cell23.CellStyle = SetCellStyle2(workbook);
                I2cell23.SetCellValue("0.7Qmax");
                ICell I2cell24 = header2Row.CreateCell(24);
                I2cell24.CellStyle = SetCellStyle2(workbook);
                I2cell24.SetCellValue("Qmax");

                ICell I2cell25 = header2Row.CreateCell(25);
                I2cell25.CellStyle = SetCellStyle2(workbook);
                I2cell25.SetCellValue("");

                ICell I2cell26 = header2Row.CreateCell(26);
                I2cell26.CellStyle = SetCellStyle2(workbook);
                I2cell26.SetCellValue("qt<=q<=qMax");


                ICell I2cell27 = header2Row.CreateCell(27);
                I2cell27.CellStyle = SetCellStyle2(workbook);
                I2cell27.SetCellValue("qMin<=q<qt ");
                ICell I2cell28 = header2Row.CreateCell(28);
                I2cell28.CellStyle = SetCellStyle2(workbook);
                I2cell28.SetCellValue("");

                ICell I2cell29 = header2Row.CreateCell(29);
                I2cell29.CellStyle = SetCellStyle2(workbook);
                I2cell29.SetCellValue("");


                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色
                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色
                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色
                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充



                //    //set date format  
                //ICellStyle cellStyleDate = workbook.CreateCellStyle();
                //IDataFormat format = workbook.CreateDataFormat();
                //cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");

                //IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行

                //ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列


                //cellTitle.SetCellValue(a_strTitle);

                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, a_strCols.Length - 1));

                //cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                //cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //使用NPOI操作Excel表  
                //NPOI.SS.UserModel.IRow row = sheet.CreateRow(1);
                int count = 0;

                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 2);
                    count = 0;
                    for (int j = 0; j < a_strCols.Count(); j++)
                    {
                        var s = a_strCols[j];
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][s].GetType();


                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {

                            cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue((Convert.ToDateTime(dt.Rows[i][s])).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][s].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }


        public static MemoryStream ExportDataTableToExcelRepairInfo(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                IRow header2Row = sheet.CreateRow(1);


                headerRow.Height = 700;




                //sheet.SetColumnWidth(0, 7 * 256);
                //sheet.SetColumnWidth(1, 10 * 256);
                //sheet.SetColumnWidth(2, 11 * 256);
                //sheet.SetColumnWidth(3, 20 * 256);
                //sheet.SetColumnWidth(4, 25 * 256);
                //sheet.SetColumnWidth(5, 25 * 256);
                //sheet.SetColumnWidth(6, 15 * 256);
                //sheet.SetColumnWidth(7, 15 * 256);
                //sheet.SetColumnWidth(8, 20 * 256);
                //sheet.SetColumnWidth(9, 20 * 256);
                //sheet.SetColumnWidth(10, 20 * 256);
                //sheet.SetColumnWidth(11, 20 * 256);
                //sheet.SetColumnWidth(12, 20 * 256);

                ICell Icell = headerRow.CreateCell(0);
                Icell.CellStyle = SetCellStyle2(workbook);
                Icell.SetCellValue("");
                ICell Icell1 = headerRow.CreateCell(1);
                Icell1.CellStyle = SetCellStyle2(workbook);
                Icell1.SetCellValue("维修记录");
                ICell Icell2 = headerRow.CreateCell(2);
                Icell2.CellStyle = SetCellStyle2(workbook);
                Icell2.SetCellValue("");
                ICell Icell3 = headerRow.CreateCell(3);
                Icell3.CellStyle = SetCellStyle2(workbook);
                Icell3.SetCellValue("");
                ICell Icell4 = headerRow.CreateCell(4);
                Icell4.CellStyle = SetCellStyle2(workbook);
                Icell4.SetCellValue("");
                ICell Icell5 = headerRow.CreateCell(5);
                Icell5.CellStyle = SetCellStyle2(workbook);
                Icell5.SetCellValue("");
                ICell Icell6 = headerRow.CreateCell(6);
                Icell6.CellStyle = SetCellStyle2(workbook);
                Icell6.SetCellValue("");
                ICell Icell7 = headerRow.CreateCell(7);
                Icell7.CellStyle = SetCellStyle2(workbook);
                Icell7.SetCellValue("");
                ICell Icell8 = headerRow.CreateCell(8);
                Icell8.CellStyle = SetCellStyle2(workbook);
                Icell8.SetCellValue("");
                ICell Icell9 = headerRow.CreateCell(9);
                Icell9.CellStyle = SetCellStyle2(workbook);
                Icell9.SetCellValue("");

                ICell Icell10 = headerRow.CreateCell(10);
                Icell10.CellStyle = SetCellStyle2(workbook);
                Icell10.SetCellValue("");
                ICell Icell11 = headerRow.CreateCell(11);
                Icell11.CellStyle = SetCellStyle2(workbook);
                Icell11.SetCellValue("");

                ICell Icell12 = headerRow.CreateCell(12);
                Icell12.CellStyle = SetCellStyle2(workbook);
                Icell12.SetCellValue("");

                ICell Icell13 = headerRow.CreateCell(13);
                Icell13.CellStyle = SetCellStyle2(workbook);
                Icell13.SetCellValue("");

                ICell Icell14 = headerRow.CreateCell(14);
                Icell14.CellStyle = SetCellStyle2(workbook);
                Icell14.SetCellValue("");

                ICell Icell15 = headerRow.CreateCell(15);
                Icell15.CellStyle = SetCellStyle2(workbook);
                Icell15.SetCellValue("");

                ICell Icell16 = headerRow.CreateCell(16);
                Icell16.CellStyle = SetCellStyle2(workbook);
                Icell16.SetCellValue("");

                ICell Icell17 = headerRow.CreateCell(17);
                Icell17.CellStyle = SetCellStyle2(workbook);
                Icell17.SetCellValue("");


                ICell Icell18 = headerRow.CreateCell(18);
                Icell18.CellStyle = SetCellStyle2(workbook);
                Icell18.SetCellValue("");

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 1, 18));

                ICell I2cell = header2Row.CreateCell(0);
                I2cell.CellStyle = SetCellStyle2(workbook);
                I2cell.SetCellValue("");
                ICell I2cell1 = header2Row.CreateCell(1);
                I2cell1.CellStyle = SetCellStyle2(workbook);
                I2cell1.SetCellValue("维修时间");
                ICell I2cell2 = header2Row.CreateCell(2);
                I2cell2.CellStyle = SetCellStyle2(workbook);
                I2cell2.SetCellValue("完成时间");
                ICell I2cell3 = header2Row.CreateCell(3);
                I2cell3.CellStyle = SetCellStyle2(workbook);
                I2cell3.SetCellValue("出厂编号");
                ICell I2cell4 = header2Row.CreateCell(4);
                I2cell4.CellStyle = SetCellStyle2(workbook);
                I2cell4.SetCellValue("类型");
                ICell I2cell5 = header2Row.CreateCell(5);
                I2cell5.CellStyle = SetCellStyle2(workbook);
                I2cell5.SetCellValue("厂家");
                ICell I2cell6 = header2Row.CreateCell(6);
                I2cell6.CellStyle = SetCellStyle2(workbook);
                I2cell6.SetCellValue("规格");
                ICell I2cell7 = header2Row.CreateCell(7);
                I2cell7.CellStyle = SetCellStyle2(workbook);
                I2cell7.SetCellValue("故障描述");
                ICell I2cell8 = header2Row.CreateCell(8);
                I2cell8.CellStyle = SetCellStyle2(workbook);
                I2cell8.SetCellValue("维修内容");
                ICell I2cell9 = header2Row.CreateCell(9);
                I2cell9.CellStyle = SetCellStyle2(workbook);
                I2cell9.SetCellValue("更换备件名称");


                ICell I2cell10 = header2Row.CreateCell(10);
                I2cell10.CellStyle = SetCellStyle2(workbook);
                I2cell10.SetCellValue("备件型号");
                ICell I2cell11 = header2Row.CreateCell(11);
                I2cell11.CellStyle = SetCellStyle2(workbook);
                I2cell11.SetCellValue("单位");

                ICell I2cell12 = header2Row.CreateCell(12);
                I2cell12.CellStyle = SetCellStyle2(workbook);
                I2cell12.SetCellValue("数量");

                ICell I2cell13 = header2Row.CreateCell(13);
                I2cell13.CellStyle = SetCellStyle2(workbook);
                I2cell13.SetCellValue("返修次数");
                ICell I2cell14 = header2Row.CreateCell(14);
                I2cell14.CellStyle = SetCellStyle2(workbook);
                I2cell14.SetCellValue("调整齿数比（原）");
                ICell I2cell15 = header2Row.CreateCell(15);
                I2cell15.CellStyle = SetCellStyle2(workbook);
                I2cell15.SetCellValue("调整齿数比（现）");
                ICell I2cell16 = header2Row.CreateCell(16);
                I2cell16.CellStyle = SetCellStyle2(workbook);
                I2cell16.SetCellValue("维修人员");
                ICell I2cell17 = header2Row.CreateCell(17);
                I2cell17.CellStyle = SetCellStyle2(workbook);
                I2cell17.SetCellValue("维修结果");
                ICell I2cell18 = header2Row.CreateCell(18);
                I2cell18.CellStyle = SetCellStyle2(workbook);
                I2cell18.SetCellValue("备注");


                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色
                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色
                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色
                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充



                //    //set date format  
                //ICellStyle cellStyleDate = workbook.CreateCellStyle();
                //IDataFormat format = workbook.CreateDataFormat();
                //cellStyleDate.DataFormat = format.GetFormat("yyyy年m月d日");

                //IRow rowTitle = sheet.CreateRow(0);//在工作表中添加一行

                //ICell cellTitle = rowTitle.CreateCell(0);//在行中添加一列


                //cellTitle.SetCellValue(a_strTitle);

                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, a_strCols.Length - 1));

                //cellTitle.CellStyle.SetFont(FontTitle1(workbook));
                //cellTitle.CellStyle.Alignment = HorizontalAlignment.CENTER;
                //使用NPOI操作Excel表  
                //NPOI.SS.UserModel.IRow row = sheet.CreateRow(1);
                int count = 0;

                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 2);
                    count = 0;
                    for (int j = 0; j < a_strCols.Count(); j++)
                    {
                        var s = a_strCols[j];
                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = typeof(string);
                        try
                        {
                            type = dt.Rows[i][s].GetType();
                        }
                        catch (Exception)
                        {
                            cell.SetCellValue("无");
                            cell.CellStyle = mHSSFCellStyle;
                            continue;
                        }


                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {

                            cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue((Convert.ToDateTime(dt.Rows[i][s])).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][s].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }


        public static MemoryStream ExportDataTableToExcelSummary(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                IRow header2Row = sheet.CreateRow(1);


                headerRow.Height = 700;
                header2Row.Height = 350;



                //sheet.SetColumnWidth(0, 7 * 256);
                //sheet.SetColumnWidth(1, 10 * 256);
                //sheet.SetColumnWidth(2, 11 * 256);
                //sheet.SetColumnWidth(3, 20 * 256);
                //sheet.SetColumnWidth(4, 25 * 256);
                //sheet.SetColumnWidth(5, 25 * 256);
                //sheet.SetColumnWidth(6, 15 * 256);
                //sheet.SetColumnWidth(7, 15 * 256);
                //sheet.SetColumnWidth(8, 20 * 256);
                //sheet.SetColumnWidth(9, 20 * 256);
                //sheet.SetColumnWidth(10, 20 * 256);
                //sheet.SetColumnWidth(11, 20 * 256);
                //sheet.SetColumnWidth(12, 20 * 256);

                ICell Icell = headerRow.CreateCell(0);
                Icell.CellStyle = SetCellStyle(workbook);
                Icell.SetCellValue("");
                ICell Icell1 = headerRow.CreateCell(1);
                Icell1.CellStyle = SetCellStyle(workbook);
                Icell1.SetCellValue("");
                ICell Icell2 = headerRow.CreateCell(2);
                Icell2.CellStyle = SetCellStyle(workbook);
                Icell2.SetCellValue("");
                ICell Icell3 = headerRow.CreateCell(3);
                Icell3.CellStyle = SetCellStyle2(workbook);
                Icell3.SetCellValue("送修日期");
                ICell Icell4 = headerRow.CreateCell(4);
                Icell4.CellStyle = SetCellStyle2(workbook);
                Icell4.SetCellValue("");
                ICell Icell5 = headerRow.CreateCell(5);
                Icell5.CellStyle = SetCellStyle2(workbook);
                Icell5.SetCellValue("");
                ICell Icell6 = headerRow.CreateCell(6);
                Icell6.CellStyle = SetCellStyle2(workbook);
                Icell6.SetCellValue("基表信息");
                ICell Icell7 = headerRow.CreateCell(7);
                Icell7.CellStyle = SetCellStyle2(workbook);
                Icell7.SetCellValue("");
                ICell Icell8 = headerRow.CreateCell(8);
                Icell8.CellStyle = SetCellStyle2(workbook);
                Icell8.SetCellValue("");
                ICell Icell9 = headerRow.CreateCell(9);
                Icell9.CellStyle = SetCellStyle2(workbook);
                Icell9.SetCellValue("");

                ICell Icell10 = headerRow.CreateCell(10);
                Icell10.CellStyle = SetCellStyle2(workbook);
                Icell10.SetCellValue("");
                ICell Icell11 = headerRow.CreateCell(11);
                Icell11.CellStyle = SetCellStyle2(workbook);
                Icell11.SetCellValue("");

                ICell Icell12 = headerRow.CreateCell(12);
                Icell12.CellStyle = SetCellStyle2(workbook);
                Icell12.SetCellValue("");

                ICell Icell13 = headerRow.CreateCell(13);
                Icell13.CellStyle = SetCellStyle2(workbook);
                Icell13.SetCellValue("");

                ICell Icell14 = headerRow.CreateCell(14);
                Icell14.CellStyle = SetCellStyle2(workbook);
                Icell14.SetCellValue("");

                ICell Icell15 = headerRow.CreateCell(15);
                Icell15.CellStyle = SetCellStyle2(workbook);
                Icell15.SetCellValue("");

                ICell Icell16 = headerRow.CreateCell(16);
                Icell16.CellStyle = SetCellStyle2(workbook);
                Icell16.SetCellValue("");

                ICell Icell17 = headerRow.CreateCell(17);
                Icell17.CellStyle = SetCellStyle2(workbook);
                Icell17.SetCellValue("");


                ICell Icell18 = headerRow.CreateCell(18);
                Icell18.CellStyle = SetCellStyle2(workbook);
                Icell18.SetCellValue("");

                ICell Icell19 = headerRow.CreateCell(19);
                Icell19.CellStyle = SetCellStyle2(workbook);
                Icell19.SetCellValue("修正仪（流量计算机）");

                ICell Icell20 = headerRow.CreateCell(20);
                Icell20.CellStyle = SetCellStyle2(workbook);
                Icell20.SetCellValue("");

                ICell Icell21 = headerRow.CreateCell(21);
                Icell21.CellStyle = SetCellStyle2(workbook);
                Icell21.SetCellValue("");

                ICell Icell22 = headerRow.CreateCell(22);
                Icell22.CellStyle = SetCellStyle2(workbook);
                Icell22.SetCellValue("");

                ICell Icell23 = headerRow.CreateCell(23);
                Icell23.CellStyle = SetCellStyle2(workbook);
                Icell23.SetCellValue("");

                ICell Icell24 = headerRow.CreateCell(24);
                Icell24.CellStyle = SetCellStyle2(workbook);
                Icell24.SetCellValue("");

                ICell Icell25 = headerRow.CreateCell(25);
                Icell25.CellStyle = SetCellStyle2(workbook);
                Icell25.SetCellValue("进厂数据");

                ICell Icell26 = headerRow.CreateCell(26);
                Icell26.CellStyle = SetCellStyle2(workbook);
                Icell26.SetCellValue("");

                ICell Icell27 = headerRow.CreateCell(27);
                Icell27.CellStyle = SetCellStyle2(workbook);
                Icell27.SetCellValue("");

                ICell Icell28 = headerRow.CreateCell(28);
                Icell28.CellStyle = SetCellStyle2(workbook);
                Icell28.SetCellValue("");

                ICell Icell29 = headerRow.CreateCell(29);
                Icell29.CellStyle = SetCellStyle2(workbook);
                Icell29.SetCellValue("进度情况");



                ICell Icell30 = headerRow.CreateCell(30);
                Icell30.CellStyle = SetCellStyle2(workbook);
                Icell30.SetCellValue("");

                ICell Icell31 = headerRow.CreateCell(31);
                Icell31.CellStyle = SetCellStyle2(workbook);
                Icell31.SetCellValue("");

                ICell Icell32 = headerRow.CreateCell(32);
                Icell32.CellStyle = SetCellStyle2(workbook);
                Icell32.SetCellValue("");

                ICell Icell33 = headerRow.CreateCell(33);
                Icell33.CellStyle = SetCellStyle2(workbook);
                Icell33.SetCellValue("");

                ICell Icell34 = headerRow.CreateCell(34);
                Icell34.CellStyle = SetCellStyle2(workbook);
                Icell34.SetCellValue("");

                ICell Icell35 = headerRow.CreateCell(35);
                Icell35.CellStyle = SetCellStyle2(workbook);
                Icell35.SetCellValue("");

                ICell Icell36 = headerRow.CreateCell(36);
                Icell36.CellStyle = SetCellStyle2(workbook);
                Icell36.SetCellValue("");

                ICell Icell37 = headerRow.CreateCell(37);
                Icell37.CellStyle = SetCellStyle2(workbook);
                Icell37.SetCellValue("清洗维修情况");

                ICell Icell38 = headerRow.CreateCell(38);
                Icell38.CellStyle = SetCellStyle2(workbook);
                Icell38.SetCellValue("");

                ICell Icell39 = headerRow.CreateCell(39);
                Icell39.CellStyle = SetCellStyle2(workbook);
                Icell39.SetCellValue("");


                ICell Icell40 = headerRow.CreateCell(40);
                Icell40.CellStyle = SetCellStyle2(workbook);
                Icell40.SetCellValue("送表信息");

                ICell Icell41 = headerRow.CreateCell(41);
                Icell41.CellStyle = SetCellStyle2(workbook);
                Icell41.SetCellValue("");

                ICell Icell42 = headerRow.CreateCell(42);
                Icell42.CellStyle = SetCellStyle2(workbook);
                Icell42.SetCellValue("");

                ICell Icell43 = headerRow.CreateCell(43);
                Icell43.CellStyle = SetCellStyle2(workbook);
                Icell43.SetCellValue("");

                ICell Icell44 = headerRow.CreateCell(44);
                Icell44.CellStyle = SetCellStyle2(workbook);
                Icell44.SetCellValue("取表信息");

                ICell Icell45 = headerRow.CreateCell(45);
                Icell45.CellStyle = SetCellStyle2(workbook);
                Icell45.SetCellValue("");

                ICell Icell46 = headerRow.CreateCell(46);
                Icell46.CellStyle = SetCellStyle2(workbook);
                Icell46.SetCellValue("");

                ICell Icell47 = headerRow.CreateCell(47);
                Icell47.CellStyle = SetCellStyle2(workbook);
                Icell47.SetCellValue("");

                ICell Icell48 = headerRow.CreateCell(48);
                Icell48.CellStyle = SetCellStyle2(workbook);
                Icell48.SetCellValue("检定信息");


                ICell Icell49 = headerRow.CreateCell(49);
                Icell49.CellStyle = SetCellStyle2(workbook);
                Icell49.SetCellValue("");

                ICell Icell50 = headerRow.CreateCell(50);
                Icell50.CellStyle = SetCellStyle2(workbook);
                Icell50.SetCellValue("奖惩计入月份");


                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 3, 5));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 6, 18));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 19, 24));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 25, 28));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 29, 36));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 37, 39));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 40, 43));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 44, 47));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 48, 49));
                ICell I2cell = header2Row.CreateCell(0);
                I2cell.CellStyle = SetCellStyle2(workbook);
                I2cell.SetCellValue("维修编号");
                ICell I2cell1 = header2Row.CreateCell(1);
                I2cell1.CellStyle = SetCellStyle2(workbook);
                I2cell1.SetCellValue("序号");
                ICell I2cell2 = header2Row.CreateCell(2);
                I2cell2.CellStyle = SetCellStyle2(workbook);
                I2cell2.SetCellValue("维修编号");
                ICell I2cell3 = header2Row.CreateCell(3);
                I2cell3.CellStyle = SetCellStyle2(workbook);
                I2cell3.SetCellValue("年");
                ICell I2cell4 = header2Row.CreateCell(4);
                I2cell4.CellStyle = SetCellStyle2(workbook);
                I2cell4.SetCellValue("月");
                ICell I2cell5 = header2Row.CreateCell(5);
                I2cell5.CellStyle = SetCellStyle2(workbook);
                I2cell5.SetCellValue("日");
                ICell I2cell6 = header2Row.CreateCell(6);
                I2cell6.CellStyle = SetCellStyle2(workbook);
                I2cell6.SetCellValue("类型");
                ICell I2cell7 = header2Row.CreateCell(7);
                I2cell7.CellStyle = SetCellStyle2(workbook);
                I2cell7.SetCellValue("厂家");
                ICell I2cell8 = header2Row.CreateCell(8);
                I2cell8.CellStyle = SetCellStyle2(workbook);
                I2cell8.SetCellValue("规格（型号）");
                ICell I2cell9 = header2Row.CreateCell(9);
                I2cell9.CellStyle = SetCellStyle2(workbook);
                I2cell9.SetCellValue("编号");


                ICell I2cell10 = header2Row.CreateCell(10);
                I2cell10.CellStyle = SetCellStyle2(workbook);
                I2cell10.SetCellValue("出厂日期(年）");
                ICell I2cell11 = header2Row.CreateCell(11);
                I2cell11.CellStyle = SetCellStyle2(workbook);
                I2cell11.SetCellValue("连接管径（DN）");

                ICell I2cell12 = header2Row.CreateCell(12);
                I2cell12.CellStyle = SetCellStyle2(workbook);
                I2cell12.SetCellValue("承压等级（MPa）");

                ICell I2cell13 = header2Row.CreateCell(13);
                I2cell13.CellStyle = SetCellStyle2(workbook);
                I2cell13.SetCellValue("流量范围（m³/h）");
                ICell I2cell14 = header2Row.CreateCell(14);
                I2cell14.CellStyle = SetCellStyle2(workbook);
                I2cell14.SetCellValue("精度等级（%）");
                ICell I2cell15 = header2Row.CreateCell(15);
                I2cell15.CellStyle = SetCellStyle2(workbook);
                I2cell15.SetCellValue("原安装地点");
                ICell I2cell16 = header2Row.CreateCell(16);
                I2cell16.CellStyle = SetCellStyle2(workbook);
                I2cell16.SetCellValue("现安装地点");
                ICell I2cell17 = header2Row.CreateCell(17);
                I2cell17.CellStyle = SetCellStyle2(workbook);
                I2cell17.SetCellValue("归属");
                ICell I2cell18 = header2Row.CreateCell(18);
                I2cell18.CellStyle = SetCellStyle2(workbook);
                I2cell18.SetCellValue("附件");
                ICell I2cell19 = header2Row.CreateCell(19);
                I2cell19.CellStyle = SetCellStyle2(workbook);
                I2cell19.SetCellValue("修正仪厂家");
                ICell I2cell20 = header2Row.CreateCell(20);
                I2cell20.CellStyle = SetCellStyle2(workbook);
                I2cell20.SetCellValue("型号");
                ICell I2cell21 = header2Row.CreateCell(21);
                I2cell21.CellStyle = SetCellStyle2(workbook);
                I2cell21.SetCellValue("编号");
                ICell I2cell22 = header2Row.CreateCell(22);
                I2cell22.CellStyle = SetCellStyle2(workbook);
                I2cell22.SetCellValue("出厂日期（年）");


                ICell I2cell23 = header2Row.CreateCell(23);
                I2cell23.CellStyle = SetCellStyle2(workbook);
                I2cell23.SetCellValue("原安装地点");
                ICell I2cell24 = header2Row.CreateCell(24);
                I2cell24.CellStyle = SetCellStyle2(workbook);
                I2cell24.SetCellValue("现安装地点");

                ICell I2cell25 = header2Row.CreateCell(25);
                I2cell25.CellStyle = SetCellStyle2(workbook);
                I2cell25.SetCellValue("字轮读数");

                ICell I2cell26 = header2Row.CreateCell(26);
                I2cell26.CellStyle = SetCellStyle2(workbook);
                I2cell26.SetCellValue("标况累计流量");


                ICell I2cell27 = header2Row.CreateCell(27);
                I2cell27.CellStyle = SetCellStyle2(workbook);
                I2cell27.SetCellValue("压力");
                ICell I2cell28 = header2Row.CreateCell(28);
                I2cell28.CellStyle = SetCellStyle2(workbook);
                I2cell28.SetCellValue("温度");

                ICell I2cell29 = header2Row.CreateCell(29);
                I2cell29.CellStyle = SetCellStyle2(workbook);
                I2cell29.SetCellValue("检测结果");

                ICell I2cell30 = header2Row.CreateCell(30);
                I2cell30.CellStyle = SetCellStyle2(workbook);
                I2cell30.SetCellValue("初测状态");
                ICell I2cell31 = header2Row.CreateCell(31);
                I2cell31.CellStyle = SetCellStyle2(workbook);
                I2cell31.SetCellValue("检测次数");
                ICell I2cell32 = header2Row.CreateCell(32);
                I2cell32.CellStyle = SetCellStyle2(workbook);
                I2cell32.SetCellValue("清洗维护维修内容");


                ICell I2cell33 = header2Row.CreateCell(33);
                I2cell33.CellStyle = SetCellStyle2(workbook);
                I2cell33.SetCellValue("清洗维护维修状态");
                ICell I2cell34 = header2Row.CreateCell(34);
                I2cell34.CellStyle = SetCellStyle2(workbook);
                I2cell34.SetCellValue("出厂前检测状态");

                ICell I2cell35 = header2Row.CreateCell(35);
                I2cell35.CellStyle = SetCellStyle2(workbook);
                I2cell35.SetCellValue("打压状态");

                ICell I2cell36 = header2Row.CreateCell(36);
                I2cell36.CellStyle = SetCellStyle2(workbook);
                I2cell36.SetCellValue("铅封号");


                ICell I2cell37 = header2Row.CreateCell(37);
                I2cell37.CellStyle = SetCellStyle2(workbook);
                I2cell37.SetCellValue("完成情况");
                ICell I2cell38 = header2Row.CreateCell(38);
                I2cell38.CellStyle = SetCellStyle2(workbook);
                I2cell38.SetCellValue("已更换配件");

                ICell I2cell39 = header2Row.CreateCell(39);
                I2cell39.CellStyle = SetCellStyle2(workbook);
                I2cell39.SetCellValue("故障说明");

                ICell I2cell40 = header2Row.CreateCell(40);
                I2cell40.CellStyle = SetCellStyle2(workbook);
                I2cell40.SetCellValue("客户名称");
                ICell I2cell41 = header2Row.CreateCell(41);
                I2cell41.CellStyle = SetCellStyle2(workbook);
                I2cell41.SetCellValue("送修人");
                ICell I2cell42 = header2Row.CreateCell(42);
                I2cell42.CellStyle = SetCellStyle2(workbook);
                I2cell42.SetCellValue("送修人联系方式");


                ICell I2cell43 = header2Row.CreateCell(43);
                I2cell43.CellStyle = SetCellStyle2(workbook);
                I2cell43.SetCellValue("承接人");
                ICell I2cell44 = header2Row.CreateCell(44);
                I2cell44.CellStyle = SetCellStyle2(workbook);
                I2cell44.SetCellValue("取表人");

                ICell I2cell45 = header2Row.CreateCell(45);
                I2cell45.CellStyle = SetCellStyle2(workbook);
                I2cell45.SetCellValue("联系电话");

                ICell I2cell46 = header2Row.CreateCell(46);
                I2cell46.CellStyle = SetCellStyle2(workbook);
                I2cell46.SetCellValue("取表日期");


                ICell I2cell47 = header2Row.CreateCell(47);
                I2cell47.CellStyle = SetCellStyle2(workbook);
                I2cell47.SetCellValue("备注");
                ICell I2cell48 = header2Row.CreateCell(48);
                I2cell48.CellStyle = SetCellStyle2(workbook);
                I2cell48.SetCellValue("检定地点");

                ICell I2cell49 = header2Row.CreateCell(49);
                I2cell49.CellStyle = SetCellStyle2(workbook);
                I2cell49.SetCellValue("送检情况");
                ICell I2cell50 = header2Row.CreateCell(50);
                I2cell50.CellStyle = SetCellStyle2(workbook);
                I2cell50.SetCellValue("");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 50, 50));

                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色
                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色
                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色
                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充



                int count = 0;

                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 2);
                    count = 0;
                    for (int j = 0; j < a_strCols.Count(); j++)
                    {
                        var s = a_strCols[j];

                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = typeof(string);
                        try
                        {
                            type = dt.Rows[i][s].GetType();
                        }
                        catch (Exception)
                        {
                            cell.SetCellValue("无");
                            cell.CellStyle = mHSSFCellStyle;
                            continue;
                        }



                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {

                            cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue((Convert.ToDateTime(dt.Rows[i][s])).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][s].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }


        public static MemoryStream ExportDataTableToExcelCheckData2(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");
                IRow headerRow = sheet.CreateRow(0);
                IRow header2Row = sheet.CreateRow(1);


                headerRow.Height = 700;
                header2Row.Height = 350;



                //sheet.SetColumnWidth(0, 7 * 256);
                //sheet.SetColumnWidth(1, 10 * 256);
                //sheet.SetColumnWidth(2, 11 * 256);
                //sheet.SetColumnWidth(3, 20 * 256);
                //sheet.SetColumnWidth(4, 25 * 256);
                //sheet.SetColumnWidth(5, 25 * 256);
                //sheet.SetColumnWidth(6, 15 * 256);
                //sheet.SetColumnWidth(7, 15 * 256);
                //sheet.SetColumnWidth(8, 20 * 256);
                //sheet.SetColumnWidth(9, 20 * 256);
                //sheet.SetColumnWidth(10, 20 * 256);
                //sheet.SetColumnWidth(11, 20 * 256);
                //sheet.SetColumnWidth(12, 20 * 256);

                ICell Icell = headerRow.CreateCell(0);
                Icell.CellStyle = SetCellStyle2(workbook);
                Icell.SetCellValue("基表信息");
                ICell Icell1 = headerRow.CreateCell(1);
                Icell1.CellStyle = SetCellStyle2(workbook);
                Icell1.SetCellValue("");
                ICell Icell2 = headerRow.CreateCell(2);
                Icell2.CellStyle = SetCellStyle2(workbook);
                Icell2.SetCellValue("");
                ICell Icell3 = headerRow.CreateCell(3);
                Icell3.CellStyle = SetCellStyle2(workbook);
                Icell3.SetCellValue("");
                ICell Icell4 = headerRow.CreateCell(4);
                Icell4.CellStyle = SetCellStyle2(workbook);
                Icell4.SetCellValue(" 测试条件              温度( ) ℃ 压力( )kpa");
                ICell Icell5 = headerRow.CreateCell(5);
                Icell5.CellStyle = SetCellStyle2(workbook);
                Icell5.SetCellValue("");
                ICell Icell6 = headerRow.CreateCell(6);
                Icell6.CellStyle = SetCellStyle2(workbook);
                Icell6.SetCellValue("");
                ICell Icell7 = headerRow.CreateCell(7);
                Icell7.CellStyle = SetCellStyle2(workbook);
                Icell7.SetCellValue("");
                ICell Icell8 = headerRow.CreateCell(8);
                Icell8.CellStyle = SetCellStyle2(workbook);
                Icell8.SetCellValue("");
                ICell Icell9 = headerRow.CreateCell(9);
                Icell9.CellStyle = SetCellStyle2(workbook);
                Icell9.SetCellValue("");

                ICell Icell10 = headerRow.CreateCell(10);
                Icell10.CellStyle = SetCellStyle2(workbook);
                Icell10.SetCellValue("");
                ICell Icell11 = headerRow.CreateCell(11);
                Icell11.CellStyle = SetCellStyle2(workbook);
                Icell11.SetCellValue("");

                ICell Icell12 = headerRow.CreateCell(12);
                Icell12.CellStyle = SetCellStyle2(workbook);
                Icell12.SetCellValue("");

                ICell Icell13 = headerRow.CreateCell(13);
                Icell13.CellStyle = SetCellStyle2(workbook);
                Icell13.SetCellValue(" 误差error%");

                ICell Icell14 = headerRow.CreateCell(14);
                Icell14.CellStyle = SetCellStyle2(workbook);
                Icell14.SetCellValue("");

                ICell Icell15 = headerRow.CreateCell(15);
                Icell15.CellStyle = SetCellStyle2(workbook);
                Icell15.SetCellValue("");

                ICell Icell16 = headerRow.CreateCell(16);
                Icell16.CellStyle = SetCellStyle2(workbook);
                Icell16.SetCellValue("");

                ICell Icell17 = headerRow.CreateCell(17);
                Icell17.CellStyle = SetCellStyle2(workbook);
                Icell17.SetCellValue("");


                ICell Icell18 = headerRow.CreateCell(18);
                Icell18.CellStyle = SetCellStyle2(workbook);
                Icell18.SetCellValue("");

                ICell Icell19 = headerRow.CreateCell(19);
                Icell19.CellStyle = SetCellStyle2(workbook);
                Icell19.SetCellValue("");

                ICell Icell20 = headerRow.CreateCell(20);
                Icell20.CellStyle = SetCellStyle2(workbook);
                Icell20.SetCellValue("");

                ICell Icell21 = headerRow.CreateCell(21);
                Icell21.CellStyle = SetCellStyle2(workbook);
                Icell21.SetCellValue("");




                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 3));

                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 4, 12));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 13, 21));

                ICell I2cell = header2Row.CreateCell(0);
                I2cell.CellStyle = SetCellStyle2(workbook);
                I2cell.SetCellValue("");
                ICell I2cell1 = header2Row.CreateCell(1);
                I2cell1.CellStyle = SetCellStyle2(workbook);
                I2cell1.SetCellValue("出厂编号");
                ICell I2cell2 = header2Row.CreateCell(2);
                I2cell2.CellStyle = SetCellStyle2(workbook);
                I2cell2.SetCellValue("证书编号");
                ICell I2cell3 = header2Row.CreateCell(3);
                I2cell3.CellStyle = SetCellStyle2(workbook);
                I2cell3.SetCellValue("测试结果备注");
                ICell I2cell4 = header2Row.CreateCell(4);
                I2cell4.CellStyle = SetCellStyle2(workbook);
                I2cell4.SetCellValue("测试条件 ");
                ICell I2cell5 = header2Row.CreateCell(5);
                I2cell5.CellStyle = SetCellStyle2(workbook);
                I2cell5.SetCellValue("1声道/r/n1path");
                ICell I2cell6 = header2Row.CreateCell(6);
                I2cell6.CellStyle = SetCellStyle2(workbook);
                I2cell6.SetCellValue("2声道/r/n2path");
                ICell I2cell7 = header2Row.CreateCell(7);
                I2cell7.CellStyle = SetCellStyle2(workbook);
                I2cell7.SetCellValue("3声道/r/n3path");
                ICell I2cell8 = header2Row.CreateCell(8);
                I2cell8.CellStyle = SetCellStyle2(workbook);
                I2cell8.SetCellValue("4声道     4path");
                ICell I2cell9 = header2Row.CreateCell(9);
                I2cell9.CellStyle = SetCellStyle2(workbook);
                I2cell9.SetCellValue("5声道     5path");


                ICell I2cell10 = header2Row.CreateCell(10);
                I2cell10.CellStyle = SetCellStyle2(workbook);
                I2cell10.SetCellValue("6声道     6path");
                ICell I2cell11 = header2Row.CreateCell(11);
                I2cell11.CellStyle = SetCellStyle2(workbook);
                I2cell11.SetCellValue("平均声速     vos");

                ICell I2cell12 = header2Row.CreateCell(12);
                I2cell12.CellStyle = SetCellStyle2(workbook);
                I2cell12.SetCellValue("理论声速    aga10");

                ICell I2cell13 = header2Row.CreateCell(13);
                I2cell13.CellStyle = SetCellStyle2(workbook);
                I2cell13.SetCellValue("误差error%");
                ICell I2cell14 = header2Row.CreateCell(14);
                I2cell14.CellStyle = SetCellStyle2(workbook);
                I2cell14.SetCellValue("1声道     1path");
                ICell I2cell15 = header2Row.CreateCell(15);
                I2cell15.CellStyle = SetCellStyle2(workbook);
                I2cell15.SetCellValue("2声道     2path");
                ICell I2cell16 = header2Row.CreateCell(16);
                I2cell16.CellStyle = SetCellStyle2(workbook);
                I2cell16.SetCellValue("3声道     3path");
                ICell I2cell17 = header2Row.CreateCell(17);
                I2cell17.CellStyle = SetCellStyle2(workbook);
                I2cell17.SetCellValue("4声道     4path");
                ICell I2cell18 = header2Row.CreateCell(18);
                I2cell18.CellStyle = SetCellStyle2(workbook);
                I2cell18.SetCellValue("5声道     5path");
                ICell I2cell19 = header2Row.CreateCell(19);
                I2cell19.CellStyle = SetCellStyle2(workbook);
                I2cell19.SetCellValue("6声道     6path");
                ICell I2cell20 = header2Row.CreateCell(20);
                I2cell20.CellStyle = SetCellStyle2(workbook);
                I2cell20.SetCellValue("平均声速     vos   ");
                ICell I2cell21 = header2Row.CreateCell(21);
                I2cell21.CellStyle = SetCellStyle2(workbook);
                I2cell21.SetCellValue("理论声速    aga10");

                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                //mHSSFCellStyle.TopBorderColor = HSSFColor.BLACK.index;    //上边框颜色
                //mHSSFCellStyle.BottomBorderColor = HSSFColor.BLACK.index;    //下边框颜色
                //mHSSFCellStyle.LeftBorderColor = HSSFColor.BLACK.index;    //左边框颜色
                //mHSSFCellStyle.RightBorderColor = HSSFColor.BLACK.index;    //右边框颜色

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充




                int count = 0;
                var c = 2;
                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    for (int q = 0; q < 2; q++)
                    {

                        NPOI.SS.UserModel.IRow rows = sheet.CreateRow(c++);
                        count = 0;
                        for (int j = 0; j < 22; j++)
                        {
                            var s = a_strCols[q].Split(',')[j];
                            NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                            Type type = dt.Rows[i][s].GetType();


                            if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                            {

                                cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                                {
                                    cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                                }
                                else
                                {
                                    if (type == typeof(DateTime))
                                    {
                                        cell.SetCellValue((Convert.ToDateTime(dt.Rows[i][s])).ToString("yyyy-MM-dd HH:mm"));
                                    }
                                    else
                                    {
                                        if (type == typeof(bool) || type == typeof(Boolean))
                                        {
                                            cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                        }
                                        else
                                        {
                                            cell.SetCellValue(dt.Rows[i][s].ToString());
                                        }
                                    }
                                }
                            }
                            //cell.CellStyle = CellStyleContent(workbook);
                            cell.CellStyle = mHSSFCellStyle;
                        }

                    }

                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(i * 2 + 2, i * 2 + 3, 0, 0));
                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(i * 2 + 2, i * 2 + 3, 1, 1));
                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(i * 2 + 2, i * 2 + 3, 2, 2));
                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(i * 2 + 2, i * 2 + 3, 3, 3));

                }

                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }



        public static MemoryStream ExportDataTableToExcelYearAnaly(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");





                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充
                int count = 0;


                count = 0;
                NPOI.SS.UserModel.IRow rows1 = sheet.CreateRow(0);
                for (int i = 0; i < a_strCols.Count(); i++)
                {

                    NPOI.SS.UserModel.ICell cell = rows1.CreateCell(count++);
                    cell.SetCellValue(a_strCols[i]);
                    cell.CellStyle = mHSSFCellStyle;
                }


                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 1);
                    count = 0;
                    for (int j = 0; j < a_strCols.Count(); j++)
                    {
                        var s = "";
                        if (j == 0)
                        {
                            s = "年份";
                        }
                        else
                        {
                            s = a_strCols[j];
                        }

                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][s].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {

                            cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(Convert.ToDateTime(dt.Rows[i][s]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][s].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }

        public static MemoryStream ExportDataTableToExcelCalibreAnaly(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");





                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充
                int count = 0;


                count = 0;
                NPOI.SS.UserModel.IRow rows1 = sheet.CreateRow(0);
                for (int i = 0; i < a_strCols.Count(); i++)
                {

                    NPOI.SS.UserModel.ICell cell = rows1.CreateCell(count++);
                    cell.SetCellValue(a_strCols[i]);
                    cell.CellStyle = mHSSFCellStyle;
                }


                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 1);
                    count = 0;
                    for (int j = 0; j < a_strCols.Count(); j++)
                    {
                        var s = "";
                        if (j == 0)
                        {
                            s = "口径";
                        }
                        else
                        {
                            s = a_strCols[j];
                        }

                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][s].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {

                            cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(Convert.ToDateTime(dt.Rows[i][s]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][s].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }



        public static MemoryStream ExportDataTableToExcelUserAnaly(DataTable dt, string a_strTitle, string[] a_strCols)
        {
            try
            {
                //文件流对象  
                MemoryStream stream = new MemoryStream();
                //打开Excel对象 
                HSSFWorkbook workbook = new HSSFWorkbook();
                //Excel的Sheet对象  
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");





                ICellStyle mHSSFCellStyle = workbook.CreateCellStyle();
                mHSSFCellStyle.Alignment = HorizontalAlignment.CENTER;// HSSFCellStyle.ALIGN_CENTER;   //左右对齐  居中
                //mHSSFCellStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;    //上下对齐  居中
                mHSSFCellStyle.BorderTop = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //上边框
                mHSSFCellStyle.BorderBottom = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;    //下边框
                mHSSFCellStyle.BorderLeft = BorderStyle.THIN;// BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //左边框
                mHSSFCellStyle.BorderRight = BorderStyle.THIN;// HSSFCellStyle.BORDER_THIN;     //右边框

                mHSSFCellStyle.WrapText = false; //自动换行  不自动换行 
                //    //mHSSFCellStyle.FillBackgroundColor = HSSFColor.WHITE.index;  //前景色    白色
                //    //mHSSFCellStyle.FillForegroundColor = HSSFColor.WHITE.index;    //背景色    白色
                //    //mHSSFCellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND; //填充方式  全部填充
                int count = 0;


                count = 0;
                NPOI.SS.UserModel.IRow rows1 = sheet.CreateRow(0);
                for (int i = 0; i < a_strCols.Count(); i++)
                {

                    NPOI.SS.UserModel.ICell cell = rows1.CreateCell(count++);
                    cell.SetCellValue(a_strCols[i]);
                    cell.CellStyle = mHSSFCellStyle;
                }


                //将数据导入到excel表中  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rows = sheet.CreateRow(i + 1);
                    count = 0;
                    for (int j = 0; j < a_strCols.Count(); j++)
                    {
                        var s = "";
                        if (j == 0)
                        {
                            s = "零件";
                        }
                        else
                        {
                            s = a_strCols[j];
                        }

                        NPOI.SS.UserModel.ICell cell = rows.CreateCell(count++);
                        Type type = dt.Rows[i][s].GetType();
                        if (type == typeof(int) || type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                        {

                            cell.SetCellValue(Convert.ToInt32(dt.Rows[i][s]));
                        }
                        else
                        {
                            if (type == typeof(float) || type == typeof(double) || type == typeof(Double))
                            {
                                cell.SetCellValue(Convert.ToDouble(dt.Rows[i][s]));
                            }
                            else
                            {
                                if (type == typeof(DateTime))
                                {
                                    cell.SetCellValue(Convert.ToDateTime(dt.Rows[i][s]).ToString("yyyy-MM-dd HH:mm"));
                                }
                                else
                                {
                                    if (type == typeof(bool) || type == typeof(Boolean))
                                    {
                                        cell.SetCellValue(Convert.ToBoolean(dt.Rows[i][s]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(dt.Rows[i][s].ToString());
                                    }
                                }
                            }
                        }
                        //cell.CellStyle = CellStyleContent(workbook);
                        cell.CellStyle = mHSSFCellStyle;
                    }
                }
                //保存excel文档  
                sheet.ForceFormulaRecalculation = true;
                workbook.Write(stream);
                //workbook.Dispose();  
                return stream;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new MemoryStream();
            }
        }



    }
}
