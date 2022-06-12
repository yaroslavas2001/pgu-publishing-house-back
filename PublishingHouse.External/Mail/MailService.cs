using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using PublishingHouse.External.Mail.Request;

namespace PublishingHouse.External.Mail;

/// <summary>
///     Todo Статика пока не заплотют
/// </summary>
public class MailService
{
	private static string From => "richlifedevelop@gmail.com";
	private static string Password => "8r5h4tx$Kdeq?z8s";

	private static List<(string, string)> ReplaceList => new()
	{
		("domain", "http://31.31.24.200:5051")
	};

	private static Dictionary<string, (string path, string caption)> EventLsit =>
		new()
		{
			{
				"registered",
				(@"D:\startup\rio-psu\rio-psu\wwwroot\EmailTriggers\registered.html" /*"wwwroot\\EmailTriggers\\registered.html"*/,
					"Welcome")
			}
		};

	public async Task RegisterSuccess(SendRegisterMail request)
	{
		await SendEvent(request.Email, "registered", new List<KeyValuePair<string, string>>
		{
			new("Token", request.Token)
		});
	}

	public async Task SendEvent(string email, string eventName, List<KeyValuePair<string, string>> replaceValue)
	{
		var body = await GetTrigger(eventName);
		SendMail(email, EventLsit[eventName].caption, UpdateBody(body, replaceValue));
	}

	private void SendMail(string mailto, string caption, string message)
	{
		try
		{
			using var mail = new MailMessage();
			mail.From = new MailAddress(From);
			mail.To.Add(new MailAddress(Regex.Replace(mailto, @"\+(.*)\@", "@")));
			mail.Subject = caption;
			mail.IsBodyHtml = true;
			mail.Body = message;
			var client = new SmtpClient();
			client.Host = "smtp.gmail.com";
			client.EnableSsl = true;
			client.Port = 587;
			client.UseDefaultCredentials = false;
			client.Credentials = new NetworkCredential(From, Password);
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.Send(mail);
		}
		catch (Exception e)
		{
			//_logger.Info("GMail not sent error:"+e.Message);
		}
	}

	private string UpdateBody(string message, List<KeyValuePair<string, string>> list)
	{
		list.ForEach(x => { message = message.Replace(ConvertKey(x.Key), x.Value); });
		ReplaceList.ForEach(x => { message = message.Replace(ConvertKey(x.Item1), x.Item2); });
		return message;
	}

	private string ConvertKey(string key)
	{
		return $"*|{key}|*";
	}

	private async Task<string> GetTrigger(string triggerName)
	{
		if (string.IsNullOrWhiteSpace(triggerName))
			throw new Exception("тригер не найден");
		if (!EventLsit.ContainsKey(triggerName))
			throw new Exception("тригер не поддерживается");
		using var reader = new StreamReader(EventLsit[triggerName].path);
		return await reader.ReadToEndAsync();
	}
}