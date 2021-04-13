using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoCardless.Resources
{

    /// <summary>
    /// Represents a billing request flow resource.
    ///
    /// Billing Request Flows can be created to enable a payer to authorise a
    /// payment created for a scheme with strong payer
    /// authorisation (such as open banking single payments).
    /// </summary>
    public class BillingRequestFlow
    {
        /// <summary>
        /// URL for a GC-controlled flow which will allow the payer to fulfil
        /// the billing request
        /// </summary>
        [JsonProperty("authorisation_url")]
        public string AuthorisationUrl { get; set; }

        /// <summary>
        /// Timestamp when the flow was created
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Timestamp when the flow will expire. Each flow currently lasts for 7
        /// days.
        /// </summary>
        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }

        /// <summary>
        /// Resources linked to this BillingRequestFlow.
        /// </summary>
        [JsonProperty("links")]
        public BillingRequestFlowLinks Links { get; set; }

        /// <summary>
        /// URL that the payer can be redirected to after completing the request
        /// flow.
        /// </summary>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
    }
    
    /// <summary>
    /// Resources linked to this BillingRequestFlow
    /// </summary>
    public class BillingRequestFlowLinks
    {
        /// <summary>
        /// ID of the [billing request](#billing-requests-billing-requests)
        /// against which this flow was created.
        /// </summary>
        [JsonProperty("billing_request")]
        public string BillingRequest { get; set; }
    }
    
}
