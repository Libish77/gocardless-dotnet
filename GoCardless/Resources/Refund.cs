using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoCardless.Resources
{

    /// <summary>
    /// Represents a refund resource.
    ///
    /// Refund objects represent (partial) refunds of a
    /// [payment](#core-endpoints-payments) back to the
    /// [customer](#core-endpoints-customers).
    /// 
    /// GoCardless will notify you via a [webhook](#appendix-webhooks) whenever
    /// a refund is created, and will update the `amount_refunded` property of
    /// the payment.
    /// </summary>
    public class Refund
    {
        /// <summary>
        /// Amount in minor unit (e.g. pence in GBP, cents in EUR).
        /// </summary>
        [JsonProperty("amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// Fixed [timestamp](#api-usage-time-zones--dates), recording when this
        /// resource was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// [ISO 4217](http://en.wikipedia.org/wiki/ISO_4217#Active_codes)
        /// currency code. This is set to the currency of the refund's
        /// [payment](#core-endpoints-payments).
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("fx")]
        public RefundFx Fx { get; set; }

        /// <summary>
        /// Unique identifier, beginning with "RF".
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Resources linked to this Refund.
        /// </summary>
        [JsonProperty("links")]
        public RefundLinks Links { get; set; }

        /// <summary>
        /// Key-value store of custom data. Up to 3 keys are permitted, with key
        /// names up to 50 characters and values up to 500 characters.
        /// </summary>
        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// An optional reference that will appear on your customer's bank
        /// statement. The character limit for this reference is dependent on
        /// the scheme.<br /> <strong>ACH</strong> - 10 characters<br />
        /// <strong>Autogiro</strong> - 11 characters<br />
        /// <strong>Bacs</strong> - 10 characters<br /> <strong>BECS</strong> -
        /// 30 characters<br /> <strong>BECS NZ</strong> - 12 characters<br />
        /// <strong>Betalingsservice</strong> - 30 characters<br />
        /// <strong>PAD</strong> - 12 characters<br /> <strong>SEPA</strong> -
        /// 140 characters <p
        /// class='restricted-notice'><strong>Restricted</strong>: You can only
        /// specify a payment reference for Bacs payments (that is, when
        /// collecting from the UK) if you're on the <a
        /// href='https://gocardless.com/pricing'>GoCardless Plus or Pro
        /// packages</a>.</p>
        /// </summary>
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
    
    public class RefundFx
    {
        /// <summary>
        /// Estimated rate that will be used in the foreign exchange of the
        /// `amount` into the `fx_currency`.
        /// This will vary based on the prevailing market rate until the moment
        /// that it is paid out.
        /// Present only before a resource is paid out.
        /// </summary>
        [JsonProperty("estimated_exchange_rate")]
        public decimal? EstimatedExchangeRate { get; set; }

        /// <summary>
        /// Rate used in the foreign exchange of the `amount` into the
        /// `fx_currency`.
        /// Present only after a resource is paid out.
        /// </summary>
        [JsonProperty("exchange_rate")]
        public decimal? ExchangeRate { get; set; }

        /// <summary>
        /// Amount that was paid out in the `fx_currency` after foreign
        /// exchange.
        /// Present only after the resource has been paid out.
        /// </summary>
        [JsonProperty("fx_amount")]
        public int? FxAmount { get; set; }

        /// <summary>
        /// [ISO 4217](http://en.wikipedia.org/wiki/ISO_4217#Active_codes) code
        /// for the currency in which amounts will be paid out (after foreign
        /// exchange). Currently "AUD", "CAD", "DKK", "EUR", "GBP", "NZD", "SEK"
        /// and "USD" are supported. Present only if payouts will be (or were)
        /// made via foreign exchange.
        /// </summary>
        [JsonProperty("fx_currency")]
        public RefundFxFxCurrency? FxCurrency { get; set; }
    }
    
    /// <summary>
    /// [ISO 4217](http://en.wikipedia.org/wiki/ISO_4217#Active_codes) code for the currency in
    /// which amounts will be paid out (after foreign exchange). Currently "AUD", "CAD", "DKK",
    /// "EUR", "GBP", "NZD", "SEK" and "USD" are supported. Present only if payouts will be (or
    /// were) made via foreign exchange.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RefundFxFxCurrency {

        /// <summary>`fx_currency` with a value of "AUD"</summary>
        [EnumMember(Value = "AUD")]
        AUD,
        /// <summary>`fx_currency` with a value of "CAD"</summary>
        [EnumMember(Value = "CAD")]
        CAD,
        /// <summary>`fx_currency` with a value of "DKK"</summary>
        [EnumMember(Value = "DKK")]
        DKK,
        /// <summary>`fx_currency` with a value of "EUR"</summary>
        [EnumMember(Value = "EUR")]
        EUR,
        /// <summary>`fx_currency` with a value of "GBP"</summary>
        [EnumMember(Value = "GBP")]
        GBP,
        /// <summary>`fx_currency` with a value of "NZD"</summary>
        [EnumMember(Value = "NZD")]
        NZD,
        /// <summary>`fx_currency` with a value of "SEK"</summary>
        [EnumMember(Value = "SEK")]
        SEK,
        /// <summary>`fx_currency` with a value of "USD"</summary>
        [EnumMember(Value = "USD")]
        USD,
    }

    /// <summary>
    /// Resources linked to this Refund
    /// </summary>
    public class RefundLinks
    {
        /// <summary>
        /// ID of the [mandate](#core-endpoints-mandates) against which the
        /// refund is being made.
        /// </summary>
        [JsonProperty("mandate")]
        public string Mandate { get; set; }

        /// <summary>
        /// ID of the [payment](#core-endpoints-payments) against which the
        /// refund is being made.
        /// </summary>
        [JsonProperty("payment")]
        public string Payment { get; set; }
    }
    
}
