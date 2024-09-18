using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EnumLibrary
    {
		public enum FormaOpodatkowania
		{
            [EnumDisplayName("Zasady ogólne")]
			ZasadyOgole,
			[EnumDisplayName("Podatek liniowy")]
			PodatekLiniowy,
			[EnumDisplayName("Ryczałt")]
			Ryczałt
		}
		public enum RodzajPozycji
        {
            [EnumDisplayName("Towar")]
            Towar = 0,
            [EnumDisplayName("Usługa")]
            Usluga = 1
        }
        public enum TypFaktury
        {
            [EnumDisplayName("Sprzedaż")]
            Sprzedaz = 0,
            [EnumDisplayName("Korekta sprzedaży")]
            SprzedazKor = 1,
            [EnumDisplayName("Zakup")]
            Zakup = 2,
            [EnumDisplayName("Korekta zakupu")]
            ZakupKor = 3,
            [EnumDisplayName("Proforma")]
            Proforma = 4
        }

        public enum JednostkaMiary
        {
            [EnumDisplayName("Sztuki")]
            Szt = 0,
            [EnumDisplayName("Kilogramy")]
            Kg = 1,
            [EnumDisplayName("Litry")]
            L = 2,
        }
        public enum StawkiPodatkuPL
        {
            [EnumDisplayName("Zwol. z opod.")]
            zw = 31,
            [EnumDisplayName("Odwrotne obciąż.")]
            oo = 32,
            [EnumDisplayName("Niepodlegające opdot.")]
            np = 33,
            [EnumDisplayName("0%")]
            Item0 = 0,
            [EnumDisplayName("3%")]
            Item3 = 3,
            [EnumDisplayName("4%")]
            Item4 = 4,
            [EnumDisplayName("7%")]
            Item7 = 7,
            [EnumDisplayName("5%")]
            Item5 = 5,
            [EnumDisplayName("8%")]
            Item8 = 8,
            [EnumDisplayName("22%")]
            Item22 = 22,
            [EnumDisplayName("23%")]
            Item23 = 23,

        }
        public enum KodyWalut
        {
            [EnumDisplayName("Zjednoczone Emiraty Arabskie")]
            AED, // United Arab Emirates Dirham

            [EnumDisplayName("Afgani (Afganistan)")]
            AFN, // Afghan Afghani

            [EnumDisplayName("Lek (Albania)")]
            ALL, // Albanian Lek

            [EnumDisplayName("Dram (Armenia)")]
            AMD, // Armenian Dram

            [EnumDisplayName("Gulden Antyle (Antyle Holenderskie)")]
            ANG, // Netherlands Antillean Guilder

            [EnumDisplayName("Kwanza (Angola)")]
            AOA, // Angolan Kwanza

            [EnumDisplayName("Peso (Argentyna)")]
            ARS, // Argentine Peso

            [EnumDisplayName("Dolar Australijski")]
            AUD, // Australian Dollar

            [EnumDisplayName("Florin arubański")]
            AWG, // Aruban Florin

            [EnumDisplayName("Manat (Azerbejdżan)")]
            AZN, // Azerbaijani Manat

            [EnumDisplayName("Marka konwertybilna (Bośnia i Hercegowina)")]
            BAM, // Bosnia-Herzegovina Convertible Mark

            [EnumDisplayName("Dolar Barbadosu")]
            BBD, // Barbadian Dollar

            [EnumDisplayName("Taka (Bangladesz)")]
            BDT, // Bangladeshi Taka

            [EnumDisplayName("Lew (Bułgaria)")]
            BGN, // Bulgarian Lev

            [EnumDisplayName("Dinar Bahrajski")]
            BHD, // Bahraini Dinar

            [EnumDisplayName("Frank Burundyjski")]
            BIF, // Burundian Franc

            [EnumDisplayName("Dolar Bermudzki")]
            BMD, // Bermudian Dollar

            [EnumDisplayName("Dolar Brunejski")]
            BND, // Brunei Dollar

            [EnumDisplayName("Boliwiano (Boliwia)")]
            BOB, // Bolivian Boliviano

            [EnumDisplayName("Real (Brazylia)")]
            BRL, // Brazilian Real

            [EnumDisplayName("Dolar Bahamski")]
            BSD, // Bahamian Dollar

            [EnumDisplayName("Ngultrum (Bhutan)")]
            BTN, // Bhutanese Ngultrum

            [EnumDisplayName("Pula (Botswana)")]
            BWP, // Botswanan Pula

            [EnumDisplayName("Rubel (Białoruś)")]
            BYN, // Belarusian Ruble

            [EnumDisplayName("Dolar Belize")]
            BZD, // Belize Dollar

            [EnumDisplayName("Dolar Kanadyjski")]
            CAD, // Canadian Dollar

            [EnumDisplayName("Frank Kongijski")]
            CDF, // Congolese Franc

            [EnumDisplayName("Frank Szwajcarski")]
            CHF, // Swiss Franc

            [EnumDisplayName("Peso chilijskie")]
            CLP, // Chilean Peso

            [EnumDisplayName("Juan Renminbi")]
            CNY, // Chinese Yuan

            [EnumDisplayName("Peso kolumbijskie")]
            COP, // Colombian Peso

            [EnumDisplayName("Kolon kostarykański")]
            CRC, // Costa Rican Colón

            [EnumDisplayName("Peso kubańskie")]
            CUP, // Cuban Peso

            [EnumDisplayName("Eskudo (Republika Zielonego Przylądka)")]
            CVE, // Cape Verdean Escudo

            [EnumDisplayName("Korona czeska")]
            CZK, // Czech Republic Koruna

            [EnumDisplayName("Frank Dżibutyjski")]
            DJF, // Djiboutian Franc

            [EnumDisplayName("Korona duńska")]
            DKK, // Danish Krone

            [EnumDisplayName("Peso dominikańskie")]
            DOP, // Dominican Peso

            [EnumDisplayName("Dinar algierski")]
            DZD, // Algerian Dinar

            [EnumDisplayName("Funt egipski")]
            EGP, // Egyptian Pound

            [EnumDisplayName("Nakfa (Erytrea)")]
            ERN, // Eritrean Nakfa

            [EnumDisplayName("Birr (Etiopia)")]
            ETB, // Ethiopian Birr

            [EnumDisplayName("Euro")]
            EUR, // Euro

            [EnumDisplayName("Dolar Fidżi")]
            FJD, // Fijian Dollar

            [EnumDisplayName("Funt Falklandzki")]
            FKP, // Falkland Islands Pound

            [EnumDisplayName("Korona farerska")]
            FOK, // Faroese Króna

            [EnumDisplayName("Funt szterling")]
            GBP, // British Pound Sterling

            [EnumDisplayName("Lari (Gruzja)")]
            GEL, // Georgian Lari

            [EnumDisplayName("Funt Guernsey")]
            GGP, // Guernsey Pound

            [EnumDisplayName("Cedi (Ghana)")]
            GHS, // Ghanaian Cedi

            [EnumDisplayName("Funt gibraltarski")]
            GIP, // Gibraltar Pound

            [EnumDisplayName("Dalasi (Gambia)")]
            GMD, // Gambian Dalasi

            [EnumDisplayName("Frank gwinejski")]
            GNF, // Guinean Franc

            [EnumDisplayName("Quetzal (Gwatemala)")]
            GTQ, // Guatemalan Quetzal

            [EnumDisplayName("Dolar gujański")]
            GYD, // Guyanaese Dollar

            [EnumDisplayName("Dolar hongkoński")]
            HKD, // Hong Kong Dollar

            [EnumDisplayName("Lempira (Honduras)")]
            HNL, // Honduran Lempira

            [EnumDisplayName("Kuna (Chorwacja)")]
            HRK, // Croatian Kuna

            [EnumDisplayName("Gourde (Haiti)")]
            HTG, // Haitian Gourde

            [EnumDisplayName("Forint (Węgry)")]
            HUF, // Hungarian Forint

            [EnumDisplayName("Rupia indonezyjska")]
            IDR, // Indonesian Rupiah

            [EnumDisplayName("Nowy izraelski szekel")]
            ILS, // Israeli New Sheqel

            [EnumDisplayName("Funt Jersey")]
            IMP, // Isle of Man Pound

            [EnumDisplayName("Rupia indyjska")]
            INR, // Indian Rupee

            [EnumDisplayName("Dinar iracki")]
            IQD, // Iraqi Dinar

            [EnumDisplayName("Rial irański")]
            IRR, // Iranian Rial

            [EnumDisplayName("Korona islandzka")]
            ISK, // Icelandic Króna

            [EnumDisplayName("Funt Jersey")]
            JEP, // Jersey Pound

            [EnumDisplayName("Dolar jamajski")]
            JMD, // Jamaican Dollar

            [EnumDisplayName("Dinar jordański")]
            JOD, // Jordanian Dinar

            [EnumDisplayName("Jen (Japonia)")]
            JPY, // Japanese Yen

            [EnumDisplayName("Szyling kenijski")]
            KES, // Kenyan Shilling

            [EnumDisplayName("Som (Kirgistan)")]
            KGS, // Kyrgystani Som

            [EnumDisplayName("Riel (Kambodża)")]
            KHR, // Cambodian Riel

            [EnumDisplayName("Dolar Kiribati")]
            KID, // Kiribati Dollar

            [EnumDisplayName("Frank Komorów")]
            KMF, // Comorian Franc

            [EnumDisplayName("Won południowokoreański")]
            KRW, // South Korean Won

            [EnumDisplayName("Dinar kuwejcki")]
            KWD, // Kuwaiti Dinar

            [EnumDisplayName("Dolar Kajmanów")]
            KYD, // Cayman Islands Dollar

            [EnumDisplayName("Tenge (Kazachstan)")]
            KZT, // Kazakhstani Tenge

            [EnumDisplayName("Kip (Laos)")]
            LAK, // Laotian Kip

            [EnumDisplayName("Funt libański")]
            LBP, // Lebanese Pound

            [EnumDisplayName("Rupia lankijska")]
            LKR, // Sri Lankan Rupee

            [EnumDisplayName("Dolar liberyjski")]
            LRD, // Liberian Dollar

            [EnumDisplayName("Loti (Lesotho)")]
            LSL, // Lesotho Loti

            [EnumDisplayName("Dinar libijski")]
            LYD, // Libyan Dinar

            [EnumDisplayName("Dirham marokański")]
            MAD, // Moroccan Dirham

            [EnumDisplayName("Lej mołdawski")]
            MDL, // Moldovan Leu

            [EnumDisplayName("Ariary (Madagaskar)")]
            MGA, // Malagasy Ariary

            [EnumDisplayName("Denar macedoński")]
            MKD, // Macedonian Denar

            [EnumDisplayName("Kyat (Myanmar)")]
            MMK, // Myanma Kyat

            [EnumDisplayName("Tugrik (Mongolia)")]
            MNT, // Mongolian Tugrik

            [EnumDisplayName("Pataca (Makau)")]
            MOP, // Macanese Pataca

            [EnumDisplayName("Ouguiya (Mauretania)")]
            MRU, // Mauritanian Ouguiya

            [EnumDisplayName("Rupia Mauritius")]
            MUR, // Mauritian Rupee

            [EnumDisplayName("Rufiyaa (Malediwy)")]
            MVR, // Maldivian Rufiyaa

            [EnumDisplayName("Kwacha malawijska")]
            MWK, // Malawian Kwacha

            [EnumDisplayName("Peso meksykańskie")]
            MXN, // Mexican Peso

            [EnumDisplayName("Ringgit (Malezja)")]
            MYR, // Malaysian Ringgit

            [EnumDisplayName("Metical (Mozambik)")]
            MZN, // Mozambican Metical

            [EnumDisplayName("Dolar namibijski")]
            NAD, // Namibian Dollar

            [EnumDisplayName("Naira (Nigeria)")]
            NGN, // Nigerian Naira

            [EnumDisplayName("Córdoba (Nikaragua)")]
            NIO, // Nicaraguan Córdoba

            [EnumDisplayName("Korona norweska")]
            NOK, // Norwegian Krone

            [EnumDisplayName("Rupia nepalska")]
            NPR, // Nepalese Rupee

            [EnumDisplayName("Dolar nowozelandzki")]
            NZD, // New Zealand Dollar

            [EnumDisplayName("Rial omański")]
            OMR, // Omani Rial

            [EnumDisplayName("Balboa (Panama)")]
            PAB, // Panamanian Balboa

            [EnumDisplayName("Nuevo Sol (Peru)")]
            PEN, // Peruvian Nuevo Sol

            [EnumDisplayName("Kina (Papua-Nowa Gwinea)")]
            PGK, // Papua New Guinean Kina

            [EnumDisplayName("Peso filipińskie")]
            PHP, // Philippine Peso

            [EnumDisplayName("Rupia pakistańska")]
            PKR, // Pakistani Rupee

            [EnumDisplayName("Złoty polski")]
            PLN, // Polish Zloty

            [EnumDisplayName("Guarani (Paragwaj)")]
            PYG, // Paraguayan Guarani

            [EnumDisplayName("Rial katarski")]
            QAR, // Qatari Rial

            [EnumDisplayName("Lej rumuński")]
            RON, // Romanian Leu

            [EnumDisplayName("Dinar serbski")]
            RSD, // Serbian Dinar

            [EnumDisplayName("Rubel (Rosja)")]
            RUB, // Russian Ruble

            [EnumDisplayName("Frank ruandyjski")]
            RWF, // Rwandan Franc

            [EnumDisplayName("Rial saudyjski")]
            SAR, // Saudi Riyal

            [EnumDisplayName("Dolar Wysp Salomona")]
            SBD, // Solomon Islands Dollar

            [EnumDisplayName("Rupia seszelska")]
            SCR, // Seychellois Rupee

            [EnumDisplayName("Funt sudański")]
            SDG, // Sudanese Pound

            [EnumDisplayName("Korona szwedzka")]
            SEK, // Swedish Krona

            [EnumDisplayName("Dolar singapurski")]
            SGD, // Singapore Dollar

            [EnumDisplayName("Funt Wyspy Świętej Heleny")]
            SHP, // Saint Helena Pound

            [EnumDisplayName("Leone (Sierra Leone)")]
            SLL, // Sierra Leonean Leone

            [EnumDisplayName("Szyling somalijski")]
            SOS, // Somali Shilling

            [EnumDisplayName("Seborgan luigino")]
            SPL, // Seborgan Luigino

            [EnumDisplayName("Dolar surinamski")]
            SRD, // Surinamese Dollar

            [EnumDisplayName("Dobra (São Tomé i Príncipe)")]
            STN, // São Tomé and Príncipe Dobra

            [EnumDisplayName("Kolón salwadorski")]
            SVC, // Salvadoran Colón

            [EnumDisplayName("Funt syryjski")]
            SYP, // Syrian Pound

            [EnumDisplayName("Lilangeni (Suazi)")]
            SZL, // Swazi Lilangeni

            [EnumDisplayName("Baht (Tajlandia)")]
            THB, // Thai Baht

            [EnumDisplayName("Somoni (Tadżykistan)")]
            TJS, // Tajikistani Somoni

            [EnumDisplayName("Manat (Turkmenistan)")]
            TMT, // Turkmenistani Manat

            [EnumDisplayName("Dinar tunezyjski")]
            TND, // Tunisian Dinar

            [EnumDisplayName("Pa'anga (Tonga)")]
            TOP, // Tongan Pa'anga

            [EnumDisplayName("Lira turecka")]
            TRY, // Turkish Lira

            [EnumDisplayName("Dolar Trynidadu i Tobago")]
            TTD, // Trinidad and Tobago Dollar

            [EnumDisplayName("Dolar Tuvalu")]
            TVD, // Tuvaluan Dollar

            [EnumDisplayName("Nowy dolar tajwański")]
            TWD, // New Taiwan Dollar

            [EnumDisplayName("Szyling tanzański")]
            TZS, // Tanzanian Shilling

            [EnumDisplayName("Hrywna (Ukraina)")]
            UAH, // Ukrainian Hryvnia

            [EnumDisplayName("Szyling ugandyjski")]
            UGX, // Ugandan Shilling

            [EnumDisplayName("Dolar amerykański")]
            USD, // United States Dollar

            [EnumDisplayName("Peso urugwajskie")]
            UYU, // Uruguayan Peso

            [EnumDisplayName("Som uzbecki")]
            UZS, // Uzbekistan Som

            [EnumDisplayName("Boliwar (Wenezuela)")]
            VES, // Venezuelan Bolívar

            [EnumDisplayName("Dong (Wietnam)")]
            VND, // Vietnamese Dong

            [EnumDisplayName("Vatu (Vanuatu)")]
            VUV, // Vanuatu Vatu

            [EnumDisplayName("Tala (Samoa)")]
            WST, // Samoan Tala

            [EnumDisplayName("Frank CFA BEAC")]
            XAF, // CFA Franc BEAC

            [EnumDisplayName("Dolar wschodniokaraibski")]
            XCD, // East Caribbean Dollar

            [EnumDisplayName("Prawa specjalne ciągnięcia")]
            XDR, // Special Drawing Rights

            [EnumDisplayName("Frank CFA BCEAO")]
            XOF, // CFA Franc BCEAO

            [EnumDisplayName("Frank CFP")]
            XPF, // CFP Franc

            [EnumDisplayName("Rial jemeński")]
            YER, // Yemeni Rial

            [EnumDisplayName("Rand (Republika Południowej Afryki)")]
            ZAR, // South African Rand

            [EnumDisplayName("Kwacha zambijska")]
            ZMW, // Zambian Kwacha

            [EnumDisplayName("Dolar Zimbabwe")]
            ZWL  // Zimbabwean Dollar
        }

        public enum CountryCodes
        {
            [EnumDisplayName("Afganistan")]
			AF,
			[EnumDisplayName("Albania")]
			AL,
			[EnumDisplayName("Algieria")]
			DZ,
			[EnumDisplayName("Andora")]
			AD,
			[EnumDisplayName("Angola")]
			AO,
			[EnumDisplayName("Antigua i Barbuda")]
			AG,
			[EnumDisplayName("Arabia Saudyjska")]
			SA,
            [EnumDisplayName("Zjednoczone Emiraty Arabskie")]
			AE,
			[EnumDisplayName("Argentyna")]
			AR,
			[EnumDisplayName("Armenia")]
			AM,
			[EnumDisplayName("Australia")]
			AU,
			[EnumDisplayName("Austria")]
			AT,
			[EnumDisplayName("Azerbejdżan")]
			AZ,
			[EnumDisplayName("Bahamy")]
			BS,
			[EnumDisplayName("Bahrajn")]
			BH,
			[EnumDisplayName("Bangladesz")]
			BD,
			[EnumDisplayName("Barbados")]
			BB,
			[EnumDisplayName("Belgia")]
			BE,
			[EnumDisplayName("Belize")]
			BZ,
			[EnumDisplayName("Benin")]
			BJ,
			[EnumDisplayName("Bhutan")]
			BT,
			[EnumDisplayName("Białoruś")]
			BY,
			[EnumDisplayName("Boliwia")]
			BO,
			[EnumDisplayName("Bośnia i Hercegowina")]
			BA,
			[EnumDisplayName("Botswana")]
			BW,
			[EnumDisplayName("Brazylia")]
			BR,
			[EnumDisplayName("Brunei")]
			BN,
			[EnumDisplayName("Bułgaria")]
			BG,
			[EnumDisplayName("Burkina Faso")]
			BF,
			[EnumDisplayName("Burundi")]
			BI,
			[EnumDisplayName("Chile")]
			CL,
			[EnumDisplayName("Chiny")]
			CN,
			[EnumDisplayName("Chorwacja")]
			HR,
			[EnumDisplayName("Cypr")]
			CY,
			[EnumDisplayName("Czad")]
			TD,
			[EnumDisplayName("Czarnogóra")]
			ME,
			[EnumDisplayName("Czechy")]
			CZ,
			[EnumDisplayName("Dania")]
			DK,
			[EnumDisplayName("Demokratyczna Republika Konga")]
			CD,
			[EnumDisplayName("Dominika")]
			DM,
			[EnumDisplayName("Dominikana")]
			DO,
			[EnumDisplayName("Dżibuti")]
			DJ,
			[EnumDisplayName("Egipt")]
			EG,
			[EnumDisplayName("Ekwador")]
			EC,
			[EnumDisplayName("Erytrea")]
			ER,
			[EnumDisplayName("Estonia")]
			EE,
			[EnumDisplayName("Etiopia")]
			ET,
			[EnumDisplayName("Fidżi")]
			FJ,
			[EnumDisplayName("Filipiny")]
			PH,
			[EnumDisplayName("Finlandia")]
			FI,
			[EnumDisplayName("Francja")]
			FR,
			[EnumDisplayName("Gabon")]
			GA,
			[EnumDisplayName("Gambia")]
			GM,
			[EnumDisplayName("Ghana")]
			GH,
			[EnumDisplayName("Grecja")]
			GR,
			[EnumDisplayName("Grenada")]
			GD,
			[EnumDisplayName("Gruzja")]
			GE,
			[EnumDisplayName("Gujana")]
			GY,
			[EnumDisplayName("Gwatemala")]
			GT,
			[EnumDisplayName("Gwinea")]
			GN,
			[EnumDisplayName("Gwinea Bissau")]
			GW,
			[EnumDisplayName("Gwinea Równikowa")]
			GQ,
			[EnumDisplayName("Haiti")]
			HT,
			[EnumDisplayName("Hiszpania")]
			ES,
			[EnumDisplayName("Holandia")]
			NL,
			[EnumDisplayName("Honduras")]
			HN,
			[EnumDisplayName("Indie")]
			IN,
			[EnumDisplayName("Indonezja")]
			ID,
			[EnumDisplayName("Irak")]
			IQ,
			[EnumDisplayName("Iran")]
			IR,
			[EnumDisplayName("Irlandia")]
			IE,
			[EnumDisplayName("Islandia")]
			IS,
			[EnumDisplayName("Izrael")]
			IL,
			[EnumDisplayName("Jamajka")]
			JM,
			[EnumDisplayName("Japonia")]
			JP,
			[EnumDisplayName("Jemen")]
			YE,
			[EnumDisplayName("Jordania")]
			JO,
			[EnumDisplayName("Kambodża")]
			KH,
			[EnumDisplayName("Kamerun")]
			CM,
			[EnumDisplayName("Kanada")]
			CA,
			[EnumDisplayName("Katar")]
			QA,
			[EnumDisplayName("Kazachstan")]
			KZ,
			[EnumDisplayName("Kenia")]
			KE,
			[EnumDisplayName("Kirgistan")]
			KG,
			[EnumDisplayName("Kiribati")]
			KI,
			[EnumDisplayName("Kolumbia")]
			CO,
			[EnumDisplayName("Komory")]
			KM,
			[EnumDisplayName("Kongo")]
			CG,
			[EnumDisplayName("Korea Południowa")]
			KR,
			[EnumDisplayName("Korea Północna")]
			KP,
			[EnumDisplayName("Kostaryka")]
			CR,
			[EnumDisplayName("Kuba")]
			CU,
			[EnumDisplayName("Kuwejt")]
			KW,
			[EnumDisplayName("Laos")]
			LA,
			[EnumDisplayName("Lesotho")]
			LS,
			[EnumDisplayName("Liban")]
			LB,
			[EnumDisplayName("Liberia")]
			LR,
			[EnumDisplayName("Libia")]
			LY,
			[EnumDisplayName("Liechtenstein")]
			LI,
			[EnumDisplayName("Litwa")]
			LT,
			[EnumDisplayName("Luksemburg")]
			LU,
			[EnumDisplayName("Łotwa")]
			LV,
			[EnumDisplayName("Macedonia")]
			MK,
			[EnumDisplayName("Madagaskar")]
			MG,
			[EnumDisplayName("Malawi")]
			MW,
			[EnumDisplayName("Malediwy")]
			MV,
			[EnumDisplayName("Malezja")]
			MY,
			[EnumDisplayName("Mali")]
			ML,
			[EnumDisplayName("Malta")]
			MT,
			[EnumDisplayName("Maroko")]
			MA,
			[EnumDisplayName("Mauretania")]
			MR,
			[EnumDisplayName("Mauritius")]
			MU,
			[EnumDisplayName("Meksyk")]
			MX,
			[EnumDisplayName("Mikronezja")]
			FM,
			[EnumDisplayName("Mołdawia")]
			MD,
			[EnumDisplayName("Monako")]
			MC,
			[EnumDisplayName("Mongolia")]
			MN,
			[EnumDisplayName("Mozambik")]
			MZ,
			[EnumDisplayName("Namibia")]
			NA,
			[EnumDisplayName("Nauru")]
			NR,
			[EnumDisplayName("Nepal")]
			NP,
			[EnumDisplayName("Niemcy")]
			DE,
			[EnumDisplayName("Niger")]
			NE,
			[EnumDisplayName("Nigeria")]
			NG,
			[EnumDisplayName("Nikaragua")]
			NI,
			[EnumDisplayName("Norwegia")]
			NO,
			[EnumDisplayName("Nowa Zelandia")]
			NZ,
			[EnumDisplayName("Oman")]
			OM,
			[EnumDisplayName("Pakistan")]
			PK,
			[EnumDisplayName("Palau")]
			PW,
			[EnumDisplayName("Panama")]
			PA,
			[EnumDisplayName("Papua-Nowa Gwinea")]
			PG,
			[EnumDisplayName("Paragwaj")]
			PY,
			[EnumDisplayName("Peru")]
			PE,
			[EnumDisplayName("Polska")]
			PL,
			[EnumDisplayName("Portugalia")]
			PT,
			[EnumDisplayName("Republika Południowej Afryki")]
			ZA,
			[EnumDisplayName("Republika Środkowoafrykańska")]
			CF,
			[EnumDisplayName("Republika Zielonego Przylądka")]
			CV,
			[EnumDisplayName("Rosja")]
			RU,
			[EnumDisplayName("Rumunia")]
			RO,
			[EnumDisplayName("Rwanda")]
			RW,
			[EnumDisplayName("Saint Kitts i Nevis")]
			KN,
			[EnumDisplayName("Saint Lucia")]
			LC,
			[EnumDisplayName("Saint Vincent i Grenadyny")]
			VC,
			[EnumDisplayName("Salwador")]
			SV,
			[EnumDisplayName("Samoa")]
			WS,
			[EnumDisplayName("San Marino")]
			SM,
			[EnumDisplayName("Senegal")]
			SN,
			[EnumDisplayName("Serbia")]
			RS,
			[EnumDisplayName("Seszele")]
			SC,
			[EnumDisplayName("Sierra Leone")]
			SL,
			[EnumDisplayName("Singapur")]
			SG,
			[EnumDisplayName("Słowacja")]
			SK,
			[EnumDisplayName("Słowenia")]
			SI,
			[EnumDisplayName("Somalia")]
			SO,
			[EnumDisplayName("Sri Lanka")]
			LK,
			[EnumDisplayName("Stany Zjednoczone")]
			US,
			[EnumDisplayName("Suazi")]
			SZ,
			[EnumDisplayName("Sudan")]
			SD,
			[EnumDisplayName("Sudan Południowy")]
			SS,
			[EnumDisplayName("Surinam")]
			SR,
			[EnumDisplayName("Syria")]
			SY,
			[EnumDisplayName("Szwajcaria")]
			CH,
			[EnumDisplayName("Szwecja")]
			SE,
			[EnumDisplayName("Tadżykistan")]
			TJ,
			[EnumDisplayName("Tajlandia")]
			TH,
			[EnumDisplayName("Tanzania")]
			TZ,
			[EnumDisplayName("Timor Wschodni")]
			TL,
			[EnumDisplayName("Togo")]
			TG,
			[EnumDisplayName("Tonga")]
			TO,
			[EnumDisplayName("Trynidad i Tobago")]
			TT,
			[EnumDisplayName("Tunezja")]
			TN,
			[EnumDisplayName("Turcja")]
			TR,
			[EnumDisplayName("Turkmenistan")]
			TM,
			[EnumDisplayName("Tuvalu")]
			TV,
			[EnumDisplayName("Uganda")]
			UG,
			[EnumDisplayName("Ukraina")]
			UA,
			[EnumDisplayName("Urugwaj")]
			UY,
			[EnumDisplayName("Uzbekistan")]
			UZ,
			[EnumDisplayName("Vanuatu")]
			VU,
			[EnumDisplayName("Watykan")]
			VA,
			[EnumDisplayName("Wenezuela")]
			VE,
			[EnumDisplayName("Węgry")]
			HU,
			[EnumDisplayName("Wielka Brytania")]
			GB,
			[EnumDisplayName("Wietnam")]
			VN,
			[EnumDisplayName("Włochy")]
			IT,
			[EnumDisplayName("Wybrzeże Kości Słoniowej")]
			CI,
			[EnumDisplayName("Wyspy Marshalla")]
			MH,
			[EnumDisplayName("Wyspy Salomona")]
			SB,
			[EnumDisplayName("Wyspy Świętego Tomasza i Książęca")]
			ST,
			[EnumDisplayName("Zambia")]
			ZM,
			[EnumDisplayName("Zimbabwe")]
			ZW,
            [EnumDisplayName("Irlandia Północna (UK)")]
            XI
		}

        public class EnumDisplayNameAttribute : Attribute
        {
            public string DisplayName { get; }

            public EnumDisplayNameAttribute(string displayName)
            {
                DisplayName = displayName;
            }
        }
    }
}
