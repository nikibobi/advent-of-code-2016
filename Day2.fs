module Day2

type Position = { X: int; Y: int }

let solve (keypad: string[]) start input =
    let inRange { X = x; Y = y } =
        y >= 0 &&
        y < keypad.Length &&
        x >= 0 &&
        x < keypad.[y].Length &&
        keypad.[y].[x] <> ' '

    let move ({ X = x; Y = y } as pos) dir =
        let pos' = match dir with
                   | 'U' -> { pos with Y = y - 1 }
                   | 'D' -> { pos with Y = y + 1 }
                   | 'L' -> { pos with X = x - 1 }
                   | 'R' -> { pos with X = x + 1 }
                   | _ -> failwith "invalid dir"
        if pos' |> inRange
        then pos'
        else pos

    let index { X = x; Y = y } =
        keypad.[y].[x]

    input
    |> Seq.scan (fun pos line -> Seq.fold move pos line) start
    |> Seq.tail
    |> Seq.map (index >> string)
    |> String.concat ""
    |> printfn "%s"

let day2 (input: seq<string>) =
    // Part 1
    solve [|"123";"456";"789"|] { X = 1; Y = 1 } input
    // Part 2
    solve [|"  1  ";" 234 ";"56789";" ABC ";"  D  "|] { X = 0; Y = 2 } input
    ()