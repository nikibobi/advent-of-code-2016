module Day4
open System
open System.Text.RegularExpressions

let parse line =
    let capture = Regex.Match(line, "^([a-z\-]+)-(\d+)\[([a-z]{5})\]$")
    let [text;roomid;checksum] = [1;2;3] |> List.map (fun i -> capture.Groups.Item(i).Value)
    (text, int roomid, checksum)

let isRealRoom (text, roomid, checksum) =
    text
    |> Seq.filter ((<>) '-')
    |> Seq.countBy id
    |> Seq.sortBy (fun (c, n) -> -n, c)
    |> Seq.map fst
    |> Seq.take 5
    |> String.Concat
    |> (=) checksum

let shiftText text amount =
    text
    |> Seq.map (function
                | '-' -> ' '
                | c -> char ((((int c - 97) + amount) % 26) + 97))
    |> String.Concat

let day4 (input: seq<string>) =
    let realRooms =
        input
        |> Seq.map parse
        |> Seq.filter isRealRoom
    // Part 1
    realRooms
    |> Seq.map (fun (_, id, _) -> id)
    |> Seq.sum
    |> printfn "%i"
    // Part 2
    realRooms
    |> Seq.map (fun (text, id, _) -> (shiftText text id, id))
    |> Seq.filter (fun (text, _) -> text.Contains("northpole"))
    |> Seq.head
    |> snd
    |> printfn "%i"
    ()