module Day7
open System
open System.Text.RegularExpressions

let (|ABBA|_|) array =
    match array with
    | [|a;b;b';a'|] when a = a' && b = b' && a <> b -> Some array
    | _ -> None

let hasABBA (s: string) =
    let isABBA =
        function
        | ABBA x -> true
        | _ -> false
    (Seq.windowed 4 s)
    |> Seq.exists (isABBA)

let solve (s: string) =
    let outside, inside = 
        s.Split('[', ']')
        |> Array.mapi (fun i s -> i, s)
        |> Array.partition (fun (i, _) -> i % 2 = 0)
    outside
    |> Array.map (snd)
    |> Array.exists (hasABBA)
    &&
    inside
    |> Array.map (snd)
    |> Array.forall (hasABBA >> not)

let day7 (input: seq<string>) =
    input
    |> Seq.map (solve)
    |> Seq.filter (id)
    |> Seq.length
    |> (printfn "%A")
    ()