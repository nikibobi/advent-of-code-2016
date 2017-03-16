module Day5
open System
open System.Text
open System.Security.Cryptography

let hasher (algo : HashAlgorithm) (s : string) =
    s
    |> Encoding.ASCII.GetBytes
    |> algo.ComputeHash
    |> Seq.map (fun c -> c.ToString("x2"))
    |> Seq.reduce (+)

let md5 = hasher (MD5.Create())

let zeroes = String('0', 5)

let find1 seed =
    let rec next i k =
        seq {
            if i < 8 then
                let key = sprintf "%s%i" seed k
                let hash = md5 key
                if hash.StartsWith(zeroes) then
                    yield hash.[5]
                    yield! next (i + 1) (k + 1)
                else
                    yield! next i (k + 1)
        }
    next 0 0

let find2 seed =
    let pass = Array.create 8 None
    let rec next k =
        if pass |> Array.contains None then
            let key = sprintf "%s%i" seed k
            let hash = md5 key
            if hash.StartsWith(zeroes) then
                let c = hash.[5]
                if '0' <= c && c <= '7' then
                    let i = Char.GetNumericValue(c) |> int
                    match pass.[i] with
                    | None -> pass.[i] <- Some hash.[6]
                    | Some _ -> ()
            next (k + 1)
    next 0
    pass |> Array.map (function Some c -> c | None -> '_')

let day5 (input: seq<string>) =
    let seed = Seq.head input
    let solve find =
        (find seed) |> Seq.iter (printf "%c")
        printfn ""

    solve find1
    solve find2
    ()