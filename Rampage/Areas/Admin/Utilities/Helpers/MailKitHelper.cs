using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Rampage.ViewModels;

namespace Rampage.Areas.Admin.Utilities.Helpers;

public class MailKitHelper
{
    private readonly IConfiguration _configuration;
    private readonly MailKitConfigVM _configurationVM;

    public MailKitHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _configurationVM = _configuration.GetSection("MailkitOptions").Get<MailKitConfigVM>();

    }

    public async Task SendEmailAsync(MailRequestVM vm)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_configurationVM.Mail);
        email.To.Add(MailboxAddress.Parse(vm.ToEmail));
        email.Subject = vm.Subject;
        var builder = new BodyBuilder();
        builder.HtmlBody = vm.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_configurationVM.Host, int.Parse(_configurationVM.Port), SecureSocketOptions.StartTls);
        smtp.Authenticate(_configurationVM.Mail, _configurationVM.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);

    }
}
