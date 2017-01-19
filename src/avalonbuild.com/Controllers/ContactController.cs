using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using avalonbuild.com.Models;

namespace avalonbuild.com.Controllers
{
	[Route("/contact")]
	public class ContactController : Controller
	{
        private readonly AppSettings _settings;

        public ContactController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

		//
		// POST: /Contact

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Index(Contact model)
		{
            try
			{
                var smtpHost = _settings.smtphost;
				var smtpPort = Int32.Parse(_settings.smtpport);
                var smtpUser = _settings.smtpuser;
                var smtpPass = _settings.smtppass;

                var destEmail = _settings.contactemail;
                var emailSubj = _settings.contactsubject;

				var message = new MimeMessage ();
				message.From.Add (new MailboxAddress (model.Name, model.Email));
				message.To.Add (new MailboxAddress ("Contact Form", destEmail));
				message.Subject = emailSubj;

				message.Body = new TextPart ("plain")
				{
					Text = model.Message
				};

				using (var client = new SmtpClient ())
				{
					client.Connect (smtpHost, smtpPort, false);

					// Note: since we don't have an OAuth2 token, disable
					// the XOAUTH2 authentication mechanism.
					client.AuthenticationMechanisms.Remove ("XOAUTH2");

					if (smtpUser != "" && smtpPass != "")
						client.Authenticate (smtpUser, smtpPass);

					client.Send (message);
					client.Disconnect (true);
				}

				return Content("Message has been sent successfully");

			}
			catch (Exception ex)
			{
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
		}
	}
}
