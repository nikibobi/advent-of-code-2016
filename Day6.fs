module Day6
open System

let frequencies (input: seq<string>) ismax =
    let n = Seq.head input |> String.length
    let maps = Array.create n Map.empty<char, int>
    let count i c =
        let m = maps.[i]
        let v =
            if Map.containsKey c m
            then m.[c] + 1
            else 1
        maps.[i] <- Map.add c v m
    input |> Seq.iter (Seq.iteri count)
    let optimal =
        if ismax
        then Seq.maxBy
        else Seq.minBy
    let mapToOne map =
        map
        |> Map.toSeq
        |> optimal snd
        |> fst
    maps |> Array.map mapToOne |> String.Concat

let day6 (input: seq<string>) =
    let solve m = printfn "%s" (frequencies input m)
    solve true
    solve false
    ()