module BingSearch

open System
open System.Net
open System.Xml
open System.Data.Services.Client

open BingSearchContainer

let private bingUrl = "https://api.datamarket.azure.com/Bing/Search"
let private key = "add from ...https://datamarket.azure.com/..."

let getBingResult query = 
    let bingContainer = new BingSearchContainer(new Uri(bingUrl)) 
    bingContainer.Credentials <- new NetworkCredential(key, key)
    let webQuery = bingContainer.Web(query, null, null, null, null, Nullable<float>(), Nullable<float>(), null)
    webQuery.Execute() |> ignore
    [
        let enumerator = webQuery.GetEnumerator()
        while enumerator.MoveNext() do
            yield enumerator.Current.Url 
    ]
