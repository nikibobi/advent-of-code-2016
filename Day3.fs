module Day3
open System

let isTriangle (a, b, c) =
    a + b > c &&
    b + c > a &&
    c + a > b

let solve data =
    data
    |> Seq.map (fun [|a;b;c|] -> (a, b, c))
    |> Seq.filter isTriangle
    |> Seq.length
    |> printfn "%i"

let day3 (input: seq<string>) =
    let data =
        input
        |> Seq.map ((fun s -> s.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)) >> Array.map int)
    // Part 1
    data
    |> solve
    // Part 2
    seq {
        yield! data |> Seq.map (fun a -> a.[0])
        yield! data |> Seq.map (fun a -> a.[1])
        yield! data |> Seq.map (fun a -> a.[2])
    }
    |> Seq.chunkBySize 3
    |> solve
    ()