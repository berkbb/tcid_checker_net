using System.Net;
using System.Text;
using System.Xml;
using CustomExtensions;
using ColorLoggerLibrary;

namespace TCIDCheckerLibrary;
/// <summary>
/// TC ID Checker class.
/// </summary>
public class TCIDChecker
{
    private readonly ColorLogger _logger = new();

    /// <summary>
    ///  Checks that Turkish ID number is correct or not.
    /// </summary>
    /// <param name="id">is TC ID</param>
    /// <param name="skipRealCitizen">is a key that controls not to create a real citizen ID. If it is true, ID will start 0, it is not correct on real life.</param>
    /// <param name="printLog"> enables / dsiables console log. If true, log will printed.</param>
    /// <param name="lvl">Print log type.</param>
    /// <returns>boolean.</returns>
    public bool controlID(string id, bool skipRealCitizen, bool printLog, LogLevel lvl)
    {
        try
        {
            bool c1 = false;
            bool c2 = false;
            bool c3 = false;

            if (id.Length == 11 && skipRealCitizen == true
                ? true
                : id[0] != '0') // If length is 11 and first number is not equal 0.
            {
                int first10Sum = 0; // First 10 digit sum.

                for (int i = 0; i < 10; i++)
                {
                    first10Sum = first10Sum + int.Parse(id[i].ToString());
                }

                int sum1 = int.Parse(id[0].ToString()) +
                    int.Parse(id[2].ToString()) +
                    int.Parse(id[4].ToString()) +
                    int.Parse(id[6].ToString()) +
                    int.Parse(
                        id[8].ToString()); // Sum of numbers at 1., 3., 5., 7. and 9. positions.

                int multiply1 = sum1 * 7; // Multiply sum1 with 7.

                int sum2 = int.Parse(id[1].ToString()) +
                    int.Parse(id[3].ToString()) +
                    int.Parse(id[5].ToString()) +
                    int.Parse(id[7].ToString()); // Sum of numbers at 2., 4., 6. and 8. positions.

                int multiply2 = sum2 * 9; // Multiply sum2 with 9.

                int operation1 = first10Sum % 10; // mod10 of first10sum.

                int operation2 = (multiply1 + multiply2) %
                    10; // mod10 of Multiply multiply1 and multiply1.

                int operation3 = (sum1 * 8) % 10; // mod10 Multiply sum1 and 8.

                if (operation1 ==
                    int.Parse(id[10].ToString())) //If operation1 is equal to 11th digit of the ID.

                {
                    c1 = true;
                }

                if (operation2 ==
                    int.Parse(id[9].ToString())) //If operation2  is equal to 10th digit of the ID.

                {
                    c2 = true;
                }

                if (operation3 ==
                    int.Parse(
                        id[10].ToString())) //If operation3 mod 10 is equal to 11th digit of the ID.

                {
                    c3 = true;
                }
            }

            bool isValid = c1 & c2 & c3;

            if (printLog == true)
            {

                _logger.Print($"TC ID: {id} is {(isValid == true ? "correct" : "wrong")}", lvl);
            }
            return isValid;
        }
        catch (Exception e)
        {
            _logger.Print($"An error occurred while checking Turkish ID - {e.Message}", LogLevel.error);
            return false;
        }
    }

    /// <summary>
    /// Generates valid random TC ID.
    /// </summary>
    /// <param name="skipRealCitizen"> is a key that controls not to create a real citizen ID. If it is true, ID will start 0, it is not correct on real life.</param>
    /// <param name="computeFake">is a key that controls compute Fake TC ID or returns a  logger.Print ready value.</param>
    /// <param name="lvl">Print log type.</param>
    /// <returns>string?.</returns>
    public string? generateID(bool skipRealCitizen, bool computeFake, LogLevel lvl)
    {
        try
        {
            var controlKey = false; // Control key for do -while.
            const int size = 11; // Size is final.
            Random random = new Random(); // Random Generator.
            var generatedNum = ""; // Generated number.

            if (skipRealCitizen == true &&
                computeFake == false) // Fake ID starts with 0.
            {
                _logger.Print(
                   "Generating fake TC ID starts with '0', so it is hard to find any match. So, the process takes a little longer. Please wait.", LogLevel.warning);
                do
                {
                    var tcList = Enumerable.Repeat(0, size).ToList<int>();
                    // Fill the list with 0.

                    // Creator logic

                    for (var i = 1; i < size - 2; i++)
                    {
                        tcList[i] = random.Next(0, 10); // Generate number for index 2-11.
                    }

                    var c1o1 = tcList[1] + tcList[3] + tcList[5] + tcList[7];
                    var c1o2 =
                        (tcList[0] + tcList[2] + tcList[4] + tcList[6] + tcList[8]) * 7 -
                            c1o1;

                    tcList[9] = c1o2 % 10;

                    var c2o1 = (tcList[0] +
                        tcList[1] +
                        tcList[2] +
                        tcList[3] +
                        tcList[4] +
                        tcList[5] +
                        tcList[6] +
                        tcList[7] +
                        tcList[8] +
                        tcList[9]);

                    tcList[10] = c2o1 % 10;

                    //
                    var buffer = new StringBuilder(); // Stringbuilder for add int to a string.
                    tcList.ForEach(t => buffer.Append(t)); // Write to builder


                    var isValidInt = Int64.TryParse(buffer.ToString(), out long z);
                    var value = buffer.ToString().PadLeft(11, '0'); // Add 0 to left.

                    if (isValidInt == true &&
                        controlID(value, true, false, lvl) ==
                            true) // Not null means valid integer && ID is correct.
                    {
                        generatedNum = value; // Set number.
                        controlKey = true; // Stop loop.
                    }
                } while (controlKey == false);

                _logger.Print($"Generated valid fake TC ID is: {generatedNum}", lvl);
            }
            else if ((skipRealCitizen == false && computeFake == true) ||
              (skipRealCitizen == true && computeFake == true)) // Print Ready Fake ID
            {
                generatedNum = "02345678982";
                _logger.Print($"Print ready valid fake TC ID is: {generatedNum}", lvl);

            }
            else
            {
                do
                {
                    var tcList = Enumerable.Repeat(0, size).ToList<int>();
                    // Fill the list with 0.

                    // Creator logic

                    tcList[0] = random.Next(1, 10); // from 1 upto 9 included. First num must not be zero.

                    for (var i = 1; i < size; i++)
                    {
                        tcList[i] = random.Next(0, 10); // Generate number for index 2-9.
                    }

                    //
                    var buffer = new StringBuilder(); // Stringbuilder for add int to a string.
                    tcList.ForEach(t => buffer.Append(t)); // Write to builder


                    var isValidInt = Int64.TryParse(buffer.ToString(), out long u);
                    var value = buffer.ToString();

                    if (isValidInt == true &&
                    controlID(value, skipRealCitizen,
                            false, lvl) == true) // Not null means valid integer && ID is correct.
                    {
                        generatedNum = value; // Set number.
                        controlKey = true; // Stop loop.
                    }

                } while (controlKey == false);
                _logger.Print($"Generated valid random TC ID is: {generatedNum!}", lvl);
            }

            return generatedNum;
        }
        catch (Exception e)
        {
            _logger.Print($"An error occurred while generating valid Turkish ID - {e.Message}", LogLevel.error);
            return null;
        }
    }

    /// <summary>
    /// Validate that Turkish ID number with given credentials from Web API.
    /// </summary>
    /// <param name="id">is TC ID.</param>
    /// <param name="name"> is user name.</param>
    /// <param name="surname"> is user surname.</param>
    /// <param name="birthYear">is user birth year.</param>
    /// <param name="skipRealCitizen">is a key that controls not to create a real citizen ID. If it is true, ID will start 0, it is not correct on real life.</param>
    /// <param name="lvl">Print log type.</param>
    /// <returns>boolean.</returns>
    public async Task<bool> validateIDAsync(string id, string name, string surname, int birthYear,
        bool skipRealCitizen, LogLevel lvl)
    {
        try
        {
            bool result = false;

            if (controlID(id, skipRealCitizen, false, lvl) == true)
            {
                var soap12Envelope = CreateSoapEnvelope_ValidateID(id, name.ToLower(), surname.ToLower(), birthYear); // Validate ID SOAP Envelope
                // _logger.Print(await soap12Envelope.ReadAsStringAsync(), LogLevel.debug);                                                                                            //  _logger.Print(await soap12Envelope.ReadAsStringAsync());
                var response = await CreateHTTPRequestAsync("https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx", soap12Envelope); // Make request.

                if (response != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response);
                    var element = doc.GetElementsByTagName("TCKimlikNoDogrulaResult").Item(0)!.InnerText;
                    result = element.parseBool(); // Parse string to boolean.
                }

            }
            _logger.Print(
               $"Person --> ID: {id}, Name: {name.ToLower()}, Surname: {surname.ToLower()}, Birth Year: {birthYear} validation result via Web API = {result}", lvl);

            return result;
        }
        catch (Exception e)
        {
            _logger.Print($"An error occurred while validating Turkish ID - {e}", LogLevel.error);
            return false;
        }
    }
    /// <summary>
    /// Validates Person and ID Card with given credentials from Web API.
    /// Returns always 'false' due to response from Web API. Service may have been stopped from authorities after Turkish people info leak.
    /// </summary>
    /// <param name="id">is TC ID.</param>
    /// <param name="name"> is user name.</param>
    /// <param name="surname"> is user surname.</param>
    /// <param name="noSurname">is person have surname or not.</param>
    /// <param name="birthDay">is user birth day.</param>
    /// <param name="noBirthDay">is person have birth day or not.</param>
    /// <param name="birthMonth">is user birth month</param>
    /// <param name="noBirthMonth">is person have birth month or not.</param>
    /// <param name="birthYear">is user birth year.</param>
    /// <param name="oldWalletSerial">is old wallet serial code.</param>
    /// <param name="oldWalletNo">is old wallet number.</param>
    /// <param name="newidCardSerial">is new TC Id Card serial number</param>
    /// <param name="skipRealCitizen">is a key that controls not to create a real citizen ID. If it is true, ID will start 0, it is not correct on real life.</param>
    /// <param name="lvl">Print log type.</param>
    /// <returns> boolean.</returns>

    public async Task<bool> validatePersonAndCardAsync(
         string id,
         string name,
         string surname,
         bool noSurname,
         int birthDay,
         bool noBirthDay,
         int birthMonth,
         bool noBirthMonth,
         int birthYear,
         string oldWalletSerial,
         int oldWalletNo,
         string newidCardSerial,
         bool skipRealCitizen, LogLevel lvl)
    {
        try
        {
            bool result = false;

            if (controlID(id, skipRealCitizen, false, lvl) == true)
            {
                var soap12Envelope = CreateSoapEnvelope_ValidatePersonAndCard(id, name.ToLower(), surname.ToLower(), noSurname.ToString().ToLower(), birthDay, noBirthDay.ToString().ToLower(), birthMonth, noBirthMonth.ToString().ToLower(), birthYear, oldWalletSerial.ToLower(), oldWalletNo, newidCardSerial.ToLower()); // Validate ID SOAP Envelope
                // _logger.Print(await soap12Envelope.ReadAsStringAsync(), LogLevel.debug);
                var response = await CreateHTTPRequestAsync("https://tckimlik.nvi.gov.tr/Service/KPSPublicV2.asmx", soap12Envelope); // Make request.

                if (response != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response);
                    var element = doc.GetElementsByTagName("KisiVeCuzdanDogrulaResult").Item(0)!.InnerText;
                    result = element.parseBool(); // Parse string to boolean.
                }

            }
            _logger.Print(
      $"Person and Card --> ID: {id}, Name: {name.ToLower()}, Surname: {surname.ToLower()}, Birth Year: {birthYear}, Birth Month: {birthMonth}, Birth Day: {birthDay},  Old Wallet No: {oldWalletNo},  Old Wallet Serial: {oldWalletSerial.ToLower()},  New ID Card Serial: {newidCardSerial.ToLower()} validation result via Web API = {result}", lvl);


            return result;
        }
        catch (Exception e)
        {
            _logger.Print($"An error occurred while validating Person and Card - {e}", LogLevel.error);
            return false;
        }



    }
    /// <summary>
    /// Validate that Foreign ID number given by Turkish authorities with given credentials from Web API.
    /// </summary>
    /// <param name="id">is TC ID.</param>
    /// <param name="name"> is user name.</param>
    /// <param name="surname"> is user surname.</param>
    ///  <param name="birthDay">is user birth day.</param>
    /// <param name="birthMonth">is user birth month</param>
    /// <param name="birthYear">is user birth year.</param>
    /// <param name="skipRealCitizen">is a key that controls not to create a real citizen ID. If it is true, ID will start 0, it is not correct on real life.</param>
    /// <param name="lvl">Print log type.</param>
    /// <returns>boolean.</returns>
    public async Task<bool> validateForeignIDAsync(string id, string name, string surname,
    int birthDay, int birthMonth, int birthYear, bool skipRealCitizen, LogLevel lvl)
    {
        try
        {
            bool result = false;

            if (controlID(id, skipRealCitizen, false, lvl) == true)
            {
                var soap12Envelope = CreateSoapEnvelope_ValidateForeignID(id, name.ToLower(), surname.ToLower(), birthDay, birthMonth, birthYear); // Validate ID SOAP Envelope
                // _logger.Print(await soap12Envelope.ReadAsStringAsync(), LogLevel.debug);
                var response = await CreateHTTPRequestAsync("https://tckimlik.nvi.gov.tr/Service/KPSPublicYabanciDogrula.asmx", soap12Envelope); // Make request.

                if (response != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response);
                    var element = doc.GetElementsByTagName("YabanciKimlikNoDogrulaResult").Item(0)!.InnerText;
                    result = element.parseBool(); // Parse string to boolean.
                }

            }
            _logger.Print(
               $"Foreign Person --> ID: {id}, Name: {name.ToLower()}, Surname: {surname.ToLower()}, Birth Year: {birthYear} validation result via Web API = {result}", lvl);

            return result;
        }
        catch (Exception e)
        {
            _logger.Print($"An error occurred while validating Foreign ID - {e}", LogLevel.error);
            return false;
        }

    }

    /// <summary>
    /// Creates HTTP request for POST SOAP Action.
    /// </summary>
    /// <param name="url">Web url string.</param>
    /// <param name="soapEnvelopeText">SOAP envelope string.</param>
    /// <returns>string?</returns>
    async Task<string?> CreateHTTPRequestAsync(string url, StringContent content)
    {
        try
        {



            using (HttpClient httpClient = new HttpClient())
            {


                using (HttpResponseMessage response = await httpClient.PostAsync(url, content))
                {
                    response.EnsureSuccessStatusCode(); // throws an Exception if 404, 500, etc.
                    return await response.Content.ReadAsStringAsync();
                }
            }

        }
        catch (Exception e)
        {
            _logger.Print($"An error occurred while http request - {e.Message}", LogLevel.error);
            return null;
        }
    }

    /// <summary>
    /// Returns validateIDAsync() SOAP envelope.
    /// </summary>
    /// <param name="id">is TC ID.</param>
    /// <param name="name"> is user name.</param>
    /// <param name="surname"> is user surname.</param>
    /// <param name="birthYear">is user birth year.</param>
    /// <returns>StringContent.</returns>
    StringContent CreateSoapEnvelope_ValidateID(string id, string name, string surname, int birthYear)

    {

        var envelopeScheme = "http://www.w3.org/2003/05/soap-envelope";
        var ws = "http://tckimlik.nvi.gov.tr/WS";

        var soapEnvelopeText = @$"<soap:Envelope xmlns:soap=""{envelopeScheme}"" xmlns:ws=""{ws}"">
    <soap:Header />
    <soap:Body>
        <ws:TCKimlikNoDogrula>
            <ws:TCKimlikNo>{id}</ws:TCKimlikNo>
            <ws:Ad>{name}</ws:Ad>
            <ws:Soyad>{surname}</ws:Soyad>
            <ws:DogumYili>{birthYear}</ws:DogumYili>
        </ws:TCKimlikNoDogrula>
    </soap:Body>
</soap:Envelope>
";
        return new StringContent(soapEnvelopeText, Encoding.UTF8, "application/soap+xml");



    }

    /// <summary>
    /// Returns validateForeignIDAsync() SOAP envelope.
    /// </summary>
    /// <param name="id">is TC ID.</param>
    /// <param name="name"> is user name.</param>
    /// <param name="surname"> is user surname.</param>
    /// <param name="birthDay">is user birth day.</param>
    ///  <param name="birthMonth">is user birth month</param>
    /// <param name="birthYear">is user birth year.</param>
    /// <returns>StringContent.</returns>
    StringContent CreateSoapEnvelope_ValidateForeignID(string id, string name, string surname,
   int birthDay, int birthMonth, int birthYear)

    {

        var envelopeScheme = "http://www.w3.org/2003/05/soap-envelope";
        var ws = "http://tckimlik.nvi.gov.tr/WS";

        var soapEnvelopeText = @$"<soap:Envelope xmlns:soap=""{envelopeScheme}"" xmlns:ws=""{ws}"">
    <soap:Header />
    <soap:Body>
        <ws:YabanciKimlikNoDogrula>
            <ws:KimlikNo>{id}</ws:KimlikNo>
            <ws:Ad>{name}</ws:Ad>
            <ws:Soyad>{surname}</ws:Soyad>
            <ws:DogumGun>{birthDay}</ws:DogumGun>
            <ws:DogumAy>{birthMonth}</ws:DogumAy>
            <ws:DogumYil>{birthYear}</ws:DogumYil>
        </ws:YabanciKimlikNoDogrula>
    </soap:Body>
</soap:Envelope>
";
        return new StringContent(soapEnvelopeText, Encoding.UTF8, "application/soap+xml");



    }

    /// <summary>
    ///  Returns validatePersonAndCardAsync() SOAP envelope.
    /// </summary>
    /// <param name="id">is TC ID.</param>
    /// <param name="name"> is user name.</param>
    /// <param name="surname"> is user surname.</param>
    /// <param name="noSurname">is person have surname or not.</param>
    /// <param name="birthDay">is user birth day.</param>
    /// <param name="noBirthDay">is person have birth day or not.</param>
    /// <param name="birthMonth">is user birth month</param>
    /// <param name="noBirthMonth">is person have birth month or not.</param>
    /// <param name="birthYear">is user birth year.</param>
    /// <param name="oldWalletSerial">is old wallet serial code.</param>
    /// <param name="oldWalletNo">is old wallet number.</param>
    /// <param name="newidCardSerial">is new TC Id Card serial number</param>
    /// <returns> StringContent.</returns>
    StringContent CreateSoapEnvelope_ValidatePersonAndCard(string id,
         string name,
         string surname,
         string noSurname,
         int birthDay,
         string noBirthDay,
         int birthMonth,
         string noBirthMonth,
         int birthYear,
         string oldWalletSerial,
         int oldWalletNo,
         string newidCardSerial)

    {

        var envelopeScheme = "http://www.w3.org/2003/05/soap-envelope";
        var ws = "http://tckimlik.nvi.gov.tr/WS";

        var soapEnvelopeText = @$"<soap:Envelope xmlns:soap=""{envelopeScheme}"" xmlns:ws=""{ws}"">
    <soap:Header />
    <soap:Body>
        <ws:KisiVeCuzdanDogrula>
            <ws:TCKimlikNo>{id}</ws:TCKimlikNo>
            <ws:Ad>{name}</ws:Ad>
            <ws:Soyad>{surname}</ws:Soyad>
            <ws:SoyadYok>{noSurname}</ws:SoyadYok>
            <ws:DogumGun>{birthDay}</ws:DogumGun>
            <ws:DogumGunYok>{noBirthDay}</ws:DogumGunYok>
            <ws:DogumAy>{birthMonth}</ws:DogumAy>
            <ws:DogumAyYok>{noBirthMonth}</ws:DogumAyYok>
            <ws:DogumYil>{birthYear}</ws:DogumYil>
            <ws:CuzdanSeri>{oldWalletSerial}</ws:CuzdanSeri>
            <ws:CuzdanNo>{oldWalletNo}</ws:CuzdanNo>
            <ws:TCKKSeriNo>{newidCardSerial}</ws:TCKKSeriNo>
        </ws:KisiVeCuzdanDogrula>
    </soap:Body>
</soap:Envelope>
";


        return new StringContent(soapEnvelopeText, Encoding.UTF8, "application/soap+xml");



    }
}




