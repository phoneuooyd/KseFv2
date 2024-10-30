using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static KseF.Models.InvoiceHeader;

namespace KseF.Services
{
    public class KsefApiService : IDisposable
	{
		public HttpClient httpClient;
		public bool isActiveConnection;
		public string ksefApiUrl;
		public string ksefApiKey;


		public KsefApiService()
		{
			httpClient = new HttpClient();

			/* 
			 * Adresy URL KseF. Na ten moment używany jest adres testowy
			 * produkcja :			https://ksef.mf.gov.pl
			 * przedprodukcyjny :   https://ksef-demo.mf.gov.pl
			 * test :				https://ksef-test.mf.gov.pl			
			 
			 * Klucze publiczne KseF
			 * produkcja :			"-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuWosgHSpiRLadA0fQbzshi5TluliZfDsJujPlyYqp6A3qnzS3WmHxtwgO58uTbemQ1HCC2qwrMwuJqR6l8tgA4ilBMDbEEtkzgbjkJ6xoEqBptgxivP/ovOFYYoAnY6brZhXytCamSvjY9KI0g0McRk24pOueXT0cbb0tlwEEjVZ8NveQNKT2c1EEE2cjmW0XB3UlIBqNqiY2rWF86DcuFDTUy+KzSmTJTFvU/ENNyLTh5kkDOmB1SY1Zaw9/Q6+a4VJ0urKZPw+61jtzWmucp4CO2cfXg9qtF6cxFIrgfbtvLofGQg09Bh7Y6ZA5VfMRDVDYLjvHwDYUHg2dPIk0wIDAQAB\r\n-----END PUBLIC KEY-----"
			 * przedprodukcyjny :   "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwocTwdNgt2+PXJ2fcB7k1kn5eFUTXBeep9pHLx6MlfkmHLvgjVpQy1/hqMTFfZqw6piFOdZMOSLgizRKjb1CtDYhWncg0mML+yhVrPyHT7bkbqfDuM2ku3q8ueEOy40SEl4jRMNvttkWnkvf/VTy2TwA9X9vTd61KJmDDZBLOCVqsyzdnELKUE8iulXwTarDvVTx4irnz/GY+y9qod+XrayYndtU6/kDgasAAQv0pu7esFFPMr83Nkqdu6JD5/0yJOl5RShQXwlmToqvpih2+L92x865/C4f3n+dZ9bgsKDGSkKSqq7Pz+QnhF7jV/JAmtJBCIMylxdxI/xfDHZ5XwIDAQAB\r\n-----END PUBLIC KEY-----"
			 * test :				"-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtCVoNVHGeaOwmzuFMiScJozTbh+ULVtQYmRNTON+20ilBOqkHrJRUZCtXUg0w+ztYMvWFr4U74ykGMnEYODT7l2F8JGuJeE9YGK8hKqaY5h0YYxJW7fWybZOxQJhwXzuasjKt/OHYWrI6SmL96bSanr6MwGNr6yiNQV3R6EFB/wpZ4scwh8ZfEs0kk29uIgZVEbkq+9n/xRQjbAtaQs6eiDb4AUOBd7nm4+Uis5goHkjTtJwmhcpQq5Vw7lug3FUsn7/luNyCVhaR4BkpB3NVexxepYSByJneFrOgOh/3GilK2a47WPAEVG3hRQAiGBUR0m7Ev7WYboQtA1TI7hc6wIDAQAB\r\n-----END PUBLIC KEY-----"
 
			 */

			ksefApiUrl = "https://ksef-test.mf.gov.pl";
			ksefApiKey = "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuWosgHSpiRLadA0fQbzshi5TluliZfDsJujPlyYqp6A3qnzS3WmHxtwgO58uTbemQ1HCC2qwrMwuJqR6l8tgA4ilBMDbEEtkzgbjkJ6xoEqBptgxivP/ovOFYYoAnY6brZhXytCamSvjY9KI0g0McRk24pOueXT0cbb0tlwEEjVZ8NveQNKT2c1EEE2cjmW0XB3UlIBqNqiY2rWF86DcuFDTUy+KzSmTJTFvU/ENNyLTh5kkDOmB1SY1Zaw9/Q6+a4VJ0urKZPw+61jtzWmucp4CO2cfXg9qtF6cxFIrgfbtvLofGQg09Bh7Y6ZA5VfMRDVDYLjvHwDYUHg2dPIk0wIDAQAB\r\n-----END PUBLIC KEY-----";
		}

		private void ProcessException(JsonElement json)
		{
			if (!json.TryGetProperty("exception", out var exceptionNode)) return;
			if (!exceptionNode.TryGetProperty("exceptionDetailList", out var exceptionList)) throw new ApplicationException($"Nieznany błąd podczas komunikacji z KSeF.\n{exceptionNode}");
			var exceptionDetail = exceptionList.EnumerateArray().FirstOrDefault();
			if (!exceptionDetail.TryGetProperty("exceptionDescription", out var exceptionDescription)) throw new ApplicationException($"Nieznany błąd podczas komunikacji z KSeF.\n{exceptionNode}");
			throw new ApplicationException($"Błąd podczas komunikacji z KSeF: {exceptionDescription.GetString()}");
		}

		private async Task<(string challenge, DateTime timestamp)> AuthorisationChallengeAsync(string nip)
		{
			var authorisationChallenge = new { contextIdentifier = new { type = "onip", identifier = nip } };
			var authorisationChallengeContent = JsonContent.Create(authorisationChallenge);
			var authorisationChallengeRequest = new HttpRequestMessage(HttpMethod.Post, ksefApiUrl + "/api/online/Session/AuthorisationChallenge") { Content = authorisationChallengeContent };
			var authorisationChallengeResponse = await httpClient.SendAsync(authorisationChallengeRequest);
			var authorisationChallengeResponseBody = await authorisationChallengeResponse.Content.ReadAsStringAsync();
			var authorisationChallengeResponseJson = JsonSerializer.Deserialize<JsonElement>(authorisationChallengeResponseBody);
			ProcessException(authorisationChallengeResponseJson);
			var challenge = PropertyOrNull(authorisationChallengeResponseJson, "challenge")?.GetString();
			var timestamp = PropertyOrNull(authorisationChallengeResponseJson, "timestamp")?.GetDateTime().ToUniversalTime();
			if (challenge == null || timestamp == null) throw new ApplicationException("Missing challenge.");
			return (challenge, timestamp.Value);
		}

		public async Task AuthenticateAsync(string nip, string authorisationToken)
		{
			(var challenge, var timestamp) = await AuthorisationChallengeAsync(nip);
			var sessionToken = await InitTokenAsync(nip, authorisationToken, challenge, timestamp);
			httpClient.DefaultRequestHeaders.Add("SessionToken", sessionToken);
			isActiveConnection = true;
			await Task.Delay(1000); 
		}

		private async Task<string> InitTokenAsync(string nip, string authorisationToken, string challenge, DateTime timestamp)
		{
			var tokenMessage = authorisationToken + "|" + (long)((timestamp - new DateTime(1970, 1, 1)).TotalMilliseconds);

			var rsa = RSA.Create();
			rsa.ImportFromPem(ksefApiKey);

			var encryptedTokenMessage = rsa.Encrypt(Encoding.UTF8.GetBytes(tokenMessage), RSAEncryptionPadding.Pkcs1);
			var encryptedTokenMessageB64 = Convert.ToBase64String(encryptedTokenMessage);

			var initToken = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<ns3:InitSessionTokenRequest
	xmlns=""http://ksef.mf.gov.pl/schema/gtw/svc/online/types/2021/10/01/0001""
	xmlns:ns2=""http://ksef.mf.gov.pl/schema/gtw/svc/types/2021/10/01/0001""
	xmlns:ns3=""http://ksef.mf.gov.pl/schema/gtw/svc/online/auth/request/2021/10/01/0001"">
	<ns3:Context>
		<Challenge>{challenge}</Challenge>
		<Identifier xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""ns2:SubjectIdentifierByCompanyType"">
			<ns2:Identifier>{nip}</ns2:Identifier>
		</Identifier>
		<DocumentType>
			<ns2:Service>KSeF</ns2:Service>
			<ns2:FormCode>
				<ns2:SystemCode>FA (2)</ns2:SystemCode>
				<ns2:SchemaVersion>1-0E</ns2:SchemaVersion>
				<ns2:TargetNamespace>http://crd.gov.pl/wzor/2021/11/29/11089/</ns2:TargetNamespace>
				<ns2:Value>FA</ns2:Value>
			</ns2:FormCode>
		</DocumentType>
		<Token>{encryptedTokenMessageB64}</Token>
	</ns3:Context>
</ns3:InitSessionTokenRequest>";

			var initTokenContent = new StringContent(initToken, Encoding.UTF8, "application/octet-stream");
			var initTokenRequest = new HttpRequestMessage(HttpMethod.Post, ksefApiUrl + "/api/online/Session/InitToken") { Content = initTokenContent };
			var initTokenResponse = await httpClient.SendAsync(initTokenRequest);
			var initTokenResponseBody = await initTokenResponse.Content.ReadAsStringAsync();
			var initTokenResponseJson = JsonSerializer.Deserialize<JsonElement>(initTokenResponseBody);
			ProcessException(initTokenResponseJson);
			var sessionToken = PropertyOrNull(initTokenResponseJson, "sessionToken", "token")?.GetString();
			return sessionToken ?? throw new ApplicationException("Missing session token.");
		}

		private async Task<(string elementReferenceNumber, string invoiceHash)> SendInvoiceAsync(string invoiceXml)
		{
			var invoiceUTF8 = Encoding.UTF8.GetBytes(invoiceXml);
			var invoiceUTF8B64 = Convert.ToBase64String(invoiceUTF8);
			var invoiceUTF8Hash = SHA256.HashData(invoiceUTF8);
			var invoiceUTF8HashB64 = Convert.ToBase64String(invoiceUTF8Hash);

			var invoiceSend = new { invoiceHash = new { hashSHA = new { algorithm = "SHA-256", encoding = "Base64", value = invoiceUTF8HashB64 }, fileSize = invoiceUTF8.Length }, invoicePayload = new { type = "plain", invoiceBody = invoiceUTF8B64 } };
			var invoiceSendContent = JsonContent.Create(invoiceSend);
			var invoiceSendRequest = new HttpRequestMessage(HttpMethod.Put, ksefApiUrl + "/api/online/Invoice/Send") { Content = invoiceSendContent };
			var invoiceSendResponse = await httpClient.SendAsync(invoiceSendRequest);
			var invoiceSendResponseBody = await invoiceSendResponse.Content.ReadAsStringAsync();
			var invoiceSendResponseJson = JsonSerializer.Deserialize<JsonElement>(invoiceSendResponseBody);
			ProcessException(invoiceSendResponseJson);
			var elementReferenceNumber = PropertyOrNull(invoiceSendResponseJson, "elementReferenceNumber")?.GetString();
			return (elementReferenceNumber ?? throw new ApplicationException("Missing reference number."), invoiceUTF8HashB64);
		}

		public async Task<(string ksefReferenceNumber, DateTime acquisitionTimestamp, string urlKSeF)> SendInvoiceAsync(string invoiceXml, CancellationToken cancellationToken)
		{
			(var elementReferenceNumber, var invoiceHash) = await SendInvoiceAsync(invoiceXml);
			while (!cancellationToken.IsCancellationRequested)
			{
				(var status, var ksefReferenceNumber, var acquisitionTimestamp) = await GetInvoiceStatusAsync(elementReferenceNumber);
				if (status == 200) return (ksefReferenceNumber!, acquisitionTimestamp!, $"{ksefApiUrl}/web/verify/{ksefReferenceNumber}/{invoiceHash.Replace("=", "%3D").Replace("/", "%2F").Replace("+", "%2B")}");
				await Task.Delay(1000, cancellationToken);
			}

			return (default, default, default);
		}


		private async Task<IReadOnlyCollection<Models.InvoiceHeader>> GetInvoicesAsync(string type, string subject, DateTime dateFrom, DateTime dateTo, int pageSize, int pageOffset)
		{
			var invoices = new List<Models.InvoiceHeader>();
			JsonContent invoiceSyncContent;
			if (type == "range") invoiceSyncContent = JsonContent.Create(new { queryCriteria = new { type, subjectType = subject, invoicingDateFrom = dateFrom.ToString("s"), invoicingDateTo = dateTo.ToString("s") } });
			else invoiceSyncContent = JsonContent.Create(new { queryCriteria = new { type, subjectType = subject, acquisitionTimestampThresholdFrom = dateFrom.ToString("s"), acquisitionTimestampThresholdTo = dateTo.ToString("s") } });
			var invoiceSyncRequest = new HttpRequestMessage(HttpMethod.Post, ksefApiUrl + $"/api/online/Query/Invoice/Sync?PageSize={pageSize}&PageOffset={pageOffset}") { Content = invoiceSyncContent };
			var invoiceSyncResponse = await httpClient.SendAsync(invoiceSyncRequest);
			var invoiceSyncResponseBody = await invoiceSyncResponse.Content.ReadAsStringAsync();
			var invoiceSyncResponseJson = JsonSerializer.Deserialize<JsonElement>(invoiceSyncResponseBody);
			ProcessException(invoiceSyncResponseJson);
			var invoiceHeaderList = PropertyOrNull(invoiceSyncResponseJson, "invoiceHeaderList") ?? throw new ApplicationException("Brak faktur w odpowiedzi.");
			foreach (var invoiceHeaderListElement in invoiceHeaderList.EnumerateArray())
			{
				var invoiceHeader = new Models.InvoiceHeader();
				invoiceHeader.ReferenceNumber = PropertyOrNull(invoiceHeaderListElement, "invoiceReferenceNumber")?.GetString();
				invoiceHeader.KsefReferenceNumber = PropertyOrNull(invoiceHeaderListElement, "ksefReferenceNumber")?.GetString()!;
				invoiceHeader.InvoicingDate = PropertyOrNull(invoiceHeaderListElement, "invoicingDate")?.GetDateTime() ?? default;
				invoiceHeader.AcquisitionTimestamp = PropertyOrNull(invoiceHeaderListElement, "acquisitionTimestamp")?.GetDateTime() ?? default;
				invoiceHeader.IssuedByNIP = PropertyOrNull(invoiceHeaderListElement, "subjectBy", "issuedByIdentifier", "identifier")?.GetString();
				invoiceHeader.IssuedByName = PropertyOrNull(invoiceHeaderListElement, "subjectBy", "issuedByName", "fullName")?.GetString();
				invoiceHeader.IssuedToNIP = PropertyOrNull(invoiceHeaderListElement, "subjectTo", "issuedToIdentifier", "identifier")?.GetString();
				invoiceHeader.IssuedToName = PropertyOrNull(invoiceHeaderListElement, "subjectTo", "issuedToName", "fullName")?.GetString() ?? PropertyOrNull(invoiceHeaderListElement, "subjectTo", "issuedToName", "firstName")?.GetString() + " " + PropertyOrNull(invoiceHeaderListElement, "subjectTo", "issuedToName", "surame")?.GetString();
				invoiceHeader.Net = Decimal.Parse(PropertyOrNull(invoiceHeaderListElement, "net")?.GetString(), CultureInfo.InvariantCulture);
				invoiceHeader.Gross = Decimal.Parse(PropertyOrNull(invoiceHeaderListElement, "gross")?.GetString(), CultureInfo.InvariantCulture);
				invoiceHeader.Vat = Decimal.Parse(PropertyOrNull(invoiceHeaderListElement, "vat")?.GetString(), CultureInfo.InvariantCulture);
				invoiceHeader.Currency = PropertyOrNull(invoiceHeaderListElement, "currency")?.GetString();
				invoiceHeader.Type = PropertyOrNull(invoiceHeaderListElement, "invoiceType")?.GetString();
				invoices.Add(invoiceHeader);
			}
			return invoices;
		}

		public async Task<IReadOnlyCollection<Models.InvoiceHeader>> GetInvoicesAsync(string type, string subject, DateTime dateFrom, DateTime dateTo)
		{
			var pageSize = 100;
			var pageOffset = 0;
			var invoices = new List<Models.InvoiceHeader>();
			while (true)
			{
				var page = await GetInvoicesAsync(type, subject, dateFrom, dateTo, pageSize, pageOffset);
				invoices.AddRange(page);
				pageOffset += page.Count;
				if (page.Count < pageSize) break;
			}
			return invoices;
		}

		public async Task<string> GetInvoiceAsync(string ksefReferenceNumber)
		{
			var fullInvoiceGetResponse = await httpClient.GetAsync(ksefApiUrl + "/api/online/Invoice/Get/" + ksefReferenceNumber);
			var fullInvoiceGetResponseBody = await fullInvoiceGetResponse.Content.ReadAsStringAsync();
			return fullInvoiceGetResponseBody;
		}

		private async Task<(int status, string ksefReferenceNumber, DateTime acquisitionTimestamp)> GetInvoiceStatusAsync(string elementReferenceNumber)
		{
			var invoiceStatusResponse = await httpClient.GetAsync(ksefApiUrl + "/api/online/Invoice/Status/" + elementReferenceNumber);
			var invoiceStatusResponseBody = await invoiceStatusResponse.Content.ReadAsStringAsync();
			var invoiceStatusResponseJson = JsonSerializer.Deserialize<JsonElement>(invoiceStatusResponseBody);
			ProcessException(invoiceStatusResponseJson);
			var processingCode = PropertyOrNull(invoiceStatusResponseJson, "processingCode")?.GetInt32() ?? throw new ApplicationException("Brak statusu wysyłki faktury.");
			var processingDescription = PropertyOrNull(invoiceStatusResponseJson, "processingDescription")?.GetString();
			if (processingCode >= 200 && processingCode <= 299)
			{
				var ksefReferenceNumber = PropertyOrNull(invoiceStatusResponseJson, "invoiceStatus", "ksefReferenceNumber")?.GetString();
				var acquisitionTimestamp = PropertyOrNull(invoiceStatusResponseJson, "invoiceStatus", "acquisitionTimestamp")?.GetDateTime();
				return (processingCode, ksefReferenceNumber, acquisitionTimestamp.Value);
			}
			else if (processingCode >= 300 && processingCode <= 399)  
				return (processingCode, null, default); 
			else //zaimplementowac komunikat z bledem
				return (processingCode, null, default);
		}

		private JsonElement? PropertyOrNull(JsonElement element, params string[] nodes)
		{
			foreach (var node in nodes)
			{
				if (!element.TryGetProperty(node, out element)) return null;
			}
			return element;
		}

		public async Task Terminate()
		{
			try
			{
				var terminateResponse = await httpClient.GetAsync(ksefApiUrl + "/api/online/Session/Terminate");
				var terminateResponseBody = await terminateResponse.Content.ReadAsStringAsync();
				isActiveConnection = false;
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Błąd podczas zamykania sesji z KSeF.", ex);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			try
			{
				if (isActiveConnection)
				{
					Terminate().ConfigureAwait(false).GetAwaiter().GetResult();
					httpClient.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Błąd podczas zamykania połączenia z KSeF. w metodzie Dispose(bool disposing)", ex);
			}
		}

		~KsefApiService()
		{
			try
			{
				Dispose(disposing: false);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Błąd podczas zamykania połączenia z KSeF. w destruktorze", ex);
			}
		}

		public void Dispose()
		{
			try
			{
				Dispose(disposing: true);
				GC.SuppressFinalize(this);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Błąd podczas zamykania połączenia z KSeF. w metodzie Dispose()", ex);
			}
		}
	}
}
