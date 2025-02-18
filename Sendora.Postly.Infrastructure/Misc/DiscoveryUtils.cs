using DnsClient;
using DnsClient.Protocol;

namespace Sendora.Core.Misc;

using System;
using System.Net;
using System.Net.NetworkInformation;

public static class DiscoveryUtils
{
    public static void Discover(string domain)
    {
        var lookup = new LookupClient();
        var imapRecord = QuerySrvRecord(lookup, $"_imap._tcp.{domain}");
        if (imapRecord != null)
        {
            Console.WriteLine($"IMAP Server: {imapRecord.Target}, Port: {imapRecord.Port}");
        }
        else
        {
            Console.WriteLine("No IMAP SRV record found.");
        }

        // Query for IMAP over SSL SRV record
        var imapSslRecord = QuerySrvRecord(lookup, $"_imap._ssl.{domain}");
        if (imapSslRecord != null)
        {
            Console.WriteLine($"IMAP SSL Server: {imapSslRecord.Target}, Port: {imapSslRecord.Port}");
        }
        else
        {
            Console.WriteLine("No IMAP SSL SRV record found.");
        }
    }
    
    private static SrvRecord? QuerySrvRecord(LookupClient lookup, string srvName)
    {
        // Perform the SRV query for the given service
        var result = lookup.Query("mail.noideaindustry.com", QueryType.SRV);

        var record = result.Answers.SrvRecords().FirstOrDefault();
        
        // Check if any SRV records are returned
        /*if (result.Answers.SrvRecords.Count > 0)
        {
            // Return the first SRV record (usually there's just one for each service)
            return result.Answers.SrvRecords()[0];
        }*/
        return null;  // No SRV record found
    }
}