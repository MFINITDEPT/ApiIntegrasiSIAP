using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Libs
{
    public class CreatePDF
    {
        public static void generatePDFfile(DbUtils db, string lsagree, string pdfLog)
        {
            String qry = "exec  BKMDB.dbo.getDataInformasiPenting @p_lsagree";
            Dictionary<String, Object> lsp = new Dictionary<string, object>();
            lsp.Add("@p_lsagree", lsagree);
            
            DataRow dr = db.GetDataTable(qry, false, lsp).Rows[0];

            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 88f, 88f, 10f, 10f);
            iTextSharp.text.Font NormalFont = FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                document.Open();

                //Header Table
                table = new PdfPTable(1);
                table.TotalWidth = 450f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 1f });

                table.AddCell(PhraseCell(new Phrase("Kepada Yth,\n", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(dr["NAMA_KONSUMEN"].ToString() + "\n", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(dr["ADDRESS1"].ToString() + " RT." + dr["RT"].ToString() + " RW." + dr["RW"] + "\n", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Kel. " + dr["KELURAHAN"].ToString() + " Kec." + dr["KECAMATAN"].ToString() + "\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Perihal : Informasi Penting\n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Debitur yang terhormat,\n", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Melalui surat ini PT MNC Finance menginformasikan bahwa sehubung dengan Perjanjian Pembiayaan atas nama\n", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Bapak/Ibu dengan PT MNC Finance, maka dengan ini disampaikan hal-hal penting sebagai berikut :\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.PaddingBottom = 10f;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 1f, 2f });
                table.TotalWidth = 380f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("No. Kontrak", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(": " + dr["LSAGREE"], NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Nama Konsumen", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(": " + dr["NAMA_KONSUMEN"], NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Angsuran", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(": " + dr["ANGSURAN"], NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Tanggal Jatuh Tempo", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(": " + dr["TANGGAL_JATUH_TEMPO"] + " Setiap Bulan", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Tanggal Jatuh Tempo Pertama", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(": " + dr["TGL_JTH_TEMPO_PERTAMA"], NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Tanggal Jatuh Tempo Akhir", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(": " + dr["TGL_JTH_TEMPO_AKHIR"], NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("Denda Keterlambatan", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(": " + dr["DENDA_KETERLAMBATAN"] + " % Per hari", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("No. Virtual Account", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(": " + dr["NO_VIRTUAL"], NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.04f, 1f });
                table.TotalWidth = 400f;
                table.LockedWidth = true;
                //table.SpacingBefore = 10f;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("A.", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("PEMBAYARAN ANGSURAN\n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 380f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("1.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Bahwa seluruh pembayaran angsuran atas pembiayaan tersebut dapat dilakukan dengan cara :\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 370f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("a.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Pembayaran melalui Bank\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 360f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("*", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Bank Central Asia (BCA)\n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 350f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("o", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Transfer lewat ATM atau setor tunai dengan menggunakan Virtual Account BCA Kombinasi \n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase(" ", NormalFont), PdfPCell.ALIGN_LEFT));
                phrase = new Phrase();
                phrase.Add(new Chunk("nomor Virtual Account : ", NormalFont));
                phrase.Add(new Chunk("Kode Virtual Account MNC Finance (00163) + 0671700041.\n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("o", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Melalui Transfer atau setoran tunai ke Rekening PT MNC Finance Cabang\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase(" ", NormalFont), PdfPCell.ALIGN_LEFT));
                phrase = new Phrase();
                phrase.Add(new Chunk(dr["NAMA_CABANG"] + " Nomor : ", NormalFont));
                phrase.Add(new Chunk(dr["NO_REKENING"] + " atas nama PT. MNC Finance.\n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.SetWidths(new float[] { 2f });
                table.TotalWidth = 360f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                phrase = new Phrase();
                phrase.Add(new Chunk("Bukti Pembayaran harus di fax ke PT MNC Finance dengan menyebutkan ", NormalFont));
                phrase.Add(new Chunk("nomor perjanjian pembiayaan, \n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("nama debitur dan pembayaran angsuran ke", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 370f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("b.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Pembayaran melalui Kantor POS, Indomaret dan Alfamart Dapat dilakukan disemua kantor pos, Indomaret\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_JUSTIFIED_ALL);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase(" ", NormalFont), PdfPCell.ALIGN_LEFT));
                phrase = new Phrase();
                phrase.Add(new Chunk("dan Alfamaret yang online dengan memberikan data : ", NormalFont));
                phrase.Add(new Chunk("nomor Perjanjian Pembiayaan, jumlah angsuran \n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase(" ", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("dan atas nama Perjanjian Pembiayaan.\n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("c.", NormalFont), PdfPCell.ALIGN_LEFT));
                phrase = new Phrase();
                phrase.Add(new Chunk("dan Alfamaret yang online dengan memberikan data : ", NormalFont));
                phrase.Add(new Chunk("nomor Perjanjian Pembiayaan, jumlah angsuran \n", NormalFont));
                table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                string AlmtCbg = string.Empty, AlmtCbg2 = string.Empty;
                AlmtCbg = dr["ALAMAT_CABANG"].ToString();
                int x = 0;
                if (AlmtCbg.Length > 70)
                {
                    x = Convert.ToInt32(dr["ALAMAT_CABANG"].ToString().Length);
                    AlmtCbg = dr["ALAMAT_CABANG"].ToString().Substring(0, 70) + "-";
                    AlmtCbg2 = dr["ALAMAT_CABANG"].ToString().Substring(70);

                    table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                    table.AddCell(PhraseCell(new Phrase("Pembayaran dapat dilakukan di : " + AlmtCbg, NormalFont), PdfPCell.ALIGN_LEFT));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 2;
                    table.AddCell(cell);


                    table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                    table.AddCell(PhraseCell(new Phrase(AlmtCbg2, NormalFont), PdfPCell.ALIGN_LEFT));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    document.Add(table);
                }
                else
                {
                    table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                    table.AddCell(PhraseCell(new Phrase("Pembayaran dapat dilakukan di : " + AlmtCbg, NormalFont), PdfPCell.ALIGN_LEFT));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    document.Add(table);
                }

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 380f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("2.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Bahwa pembayaran melalui kolektor dikenakan biaya penagihan Setiap pembayaran angsuran melalui kolektor\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("WAJIB MEMBAYAR BIAYA PENAGIHAN. Besarnya biaya penagihan pembiayaan mobil/rumah (KPR)\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("minimal sebesar Rp. 30.000,- (tiga puluh ribu rupiah)\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                table.AddCell(PhraseCell(new Phrase("3.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Bahwa apabila pembayaran angsuran/pelunasan tidak dilakukan kepada PT MNC Finance, maka PT MNC)\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Finance tidak bertanggungjawab dan tidak mengakui pembayaran tersebut serta apabila menurut catatan PT\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("MNC Finance angsuran Bapak/Ibu telah menunggak, maka PT MNC Finance akan melaksanakan tindakan-\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("tindakan yang dianggap perlu.\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.04f, 1f });
                table.TotalWidth = 400f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("B.", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("ASURANSI\n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                phrase = new Phrase();
                phrase.Add(new Chunk("Semua kendaraan bermotor di PT MNC Finance telah diasuransikan.", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                phrase.Add(new Chunk("Berikut adalah hal-hal yang perlu \n", NormalFont));
                table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 380f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("diperhatikan apabila terjadi kehilangan kendaraan bermotor :\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 380f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("1.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Segera menginformasikan kepada PT MNC Finance pada hari kejadian agar dapat memperoleh informasi untuk\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("proses selanjutnya, baik untuk Laporan kepolisian ataupun proses pengurusan asuransi;\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("2.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Masa belaku polis asuransi adalah saat kendaraan bermotor diterima sampai dengan habis masa pembiayaan.\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("3.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Kasus yang akan mendapatkan penggantian dari asuransi adalah Pencurian;\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("4.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Atas terjadinya kehilangan kendaraan bermotor, maka pihak kepolisian dan asuransi akan melakukan survey ke\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("lokasi kejadian (dengan ataupun tanpa pemberitahuan terlebih dahulu).\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("5.", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Hal-hal yang tidak dijamin dalam polis asuransi sehingga tidak ada penggantian kerugian adalah sebagai berikut :\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.03f, 1f });
                table.TotalWidth = 370f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("*", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Kasus kehilangan yang disebabkan oleh penipuan dan penggelapan\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("*", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Kendaraan bermotor dialihkan kepada pihak lain tanpa memberitahukan secara resmi kepada PT MNC \n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Finance. Setiap pengalihan pembiayaan wajib diberitahukan secara resmi ke PT MNC Finance.\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.04f, 1f });
                table.TotalWidth = 400f;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(PhraseCell(new Phrase("C.", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("BILIK ADUAN (HOTLINE MNC)\n", FontFactory.GetFont("Times-Roman", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Sampaikan informasi yang Bapak/Ibu ketahui mengenai penyimpanan yang dilakukan oleh karyawan PT MNC\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Finance (diantaranya : meminta uang jasa/imbalan,dsbnya) dengan telepon atau SMS ke (021) 29701100 (berlaku tarif\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 2;
                table.AddCell(cell);

                table.AddCell(PhraseCell(new Phrase("", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("nominal). identitas pengirim dan informasi yang diberikan akan terjamin kerahasiaannya.\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.PaddingBottom = 5f;
                cell.Colspan = 2;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 430f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 1f });

                table.AddCell(PhraseCell(new Phrase("Demikian hal penting yang PT MNC Finance sampaikan, apabila ada hal-hal yang ingin Bapak/Ibu tanyakan dapat \n", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("menghubungi PT MNC Finance sesuai dengan alamat yang tercantum diatas pada jam kerja.\n", NormalFont), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("Atas perhatian dan kerjasamanya, PT MNC Finance mengucapkan terima kasih.\n", NormalFont), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_JUSTIFIED_ALL);
                cell.PaddingBottom = 10f;
                table.AddCell(cell);
                document.Add(table);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                MemoryStream ms = new MemoryStream(bytes);
                FileStream fs = new FileStream(pdfLog, FileMode.Create);

                ms.WriteTo(fs);

                ms.Close();
                fs.Close();
                fs.Dispose();
                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.ContentType = "application/pdf";
                //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
                //HttpContext.Current.Response.ContentType = "application/pdf";
                //HttpContext.Current.Response.Buffer = true;
                //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //HttpContext.Current.Response.BinaryWrite(bytes);
                //HttpContext.Current.Response.End();
                //HttpContext.Current.Response.Close();
            }
        }

        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase)
            {
                BorderColor = BaseColor.WHITE,
                VerticalAlignment = PdfPCell.ALIGN_TOP,
                HorizontalAlignment = align,
                PaddingBottom = 2f,
                PaddingTop = 0f
            };
            return cell;
        }

        //public static void MergeDOCX(String query, string srcFile, string rsultFile)
        //{
        //    DataSet ds = CDA.GetDataSet(query);
        //    DataRow _dr = ds.Tables[0].Rows[0];

        //    using (Bytescout.Document.Document doc = new Bytescout.Document.Document())
        //    {
        //        doc.Open(srcFile);
        //        foreach (DataColumn c in _dr.Table.Columns)
        //        {
        //            doc.ReplaceText("%" + c.ColumnName.ToUpper() + "%", _dr[c].ToString());
        //        }
        //        doc.Save(rsultFile);
        //    }
        //}

        public static DateTime CurrentDateTime
        {
            get
            {
                int hour = DateTime.Now.Hour;
                int min = DateTime.Now.Minute;
                int second = DateTime.Now.Second;
                DateTime dt = new DateTime(Convert.ToInt16(CurrentYear), Convert.ToInt16(CurrentMonth), Convert.ToInt16(CurrentDay), hour, min, second, DateTime.Now.Kind);
                return dt;
            }
        }

        public static string CurrentYear
        {

            get { return DateTime.Now.Year.ToString(); }
            //get { return (HttpContext.Current.Session[SessionKey.CURRENT_DATE].ToString()).Substring(6,4); }
            // get { return Utility.CurrentDateTime.ToString("yyyy"); }
        }
        public static string CurrentMonth
        {
            get { return DateTime.Now.Month.ToString(); }
            //get { return (HttpContext.Current.Session[SessionKey.CURRENT_DATE].ToString()).Substring(3,2); }
            //get { return Utility.CurrentDateTime.ToString("MM"); }
        }
        public static string CurrentDay
        {
            get { return DateTime.Now.Day.ToString(); }
            //get { return Utility.CurrentDateTime.ToString("dd"); }
        }
        public static string CurrentDate
        {  
                get { return DateTime.Now.Date.ToString("dd/MM/yyyy"); }
                //get { return Utility.CurrentDateTime.ToString("dd/MM/yyyy"); }
            
        }
    }
}
