using MailKit.Security;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

public class Correos
{

    public string servidor { get; set; }

    public int puerto { get; set; }
    public string remitente { get; set; }
    public string pass { get; set; }



    public void LeerXML()

    {
        XDocument doc = XDocument.Load("C:\\Users\\krusn\\source\\repos\\Ver2\\XML\\confCorreos.xml");


        var correo = from lv1 in doc.Descendants("Correo")
                     select lv1.Attribute("correo").Value;
        XDocument doc1 = XDocument.Load("C:\\Users\\krusn\\source\\repos\\Ver2\\XML\\confCorreos.xml");

        var passw = from lv1 in doc1.Descendants("Pass")
                    select lv1.Attribute("pass").Value;
        XDocument doc2 = XDocument.Load("C:\\Users\\krusn\\source\\repos\\Ver2\\XML\\confCorreos.xml");

        var jornada = from lv1 in doc2.Descendants("Jornada")
                      select lv1.Attribute("ornada").Value;

        remitente = correo.First();
        pass = passw.First();



    }
    public Correos(string Cdestino, string usuarioN, string asunto, string message)
    {
        servidor = "smtp.gmail.com";
        puerto = 587;
        LeerXML();
        MimeMessage mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress("DimarioTimes", remitente));
        mensaje.To.Add(new MailboxAddress(usuarioN, Cdestino));
        mensaje.Subject = asunto;
        BodyBuilder cuerpo = new BodyBuilder();
        cuerpo.TextBody = message;
        mensaje.Body = cuerpo.ToMessageBody();

        SmtpClient Cliente = new SmtpClient();
        Cliente.CheckCertificateRevocation = false;
        Cliente.Connect(servidor, puerto, SecureSocketOptions.StartTls);
        Cliente.Authenticate(remitente, pass);
        Cliente.Send(mensaje);
        Cliente.Disconnect(true);
    }





}


