namespace App

open Feliz
open Feliz.MaterialUI

type Components =
    /// <summary>
    /// A USA SSN input mask component.
    /// </summary>
    [<ReactComponent>]
    static member TestInputMask () =
        let ssn, setSsn = React.useState("888776666")
        let handleChange s =
            printfn "%s" s
            setSsn s

        Mui.grid [
            grid.container true
            grid.children [
                Mui.grid [
                    grid.item true
                    grid.xs._2
                    grid.children [
                        Mui.textField [
                            textField.variant.outlined
                            textField.label "Display Only"
                            textField.value ssn
                        ]
                    ]
                ]
                Mui.grid [
                    grid.item true
                    grid.xs._2
                    grid.children [
                        InputMask.inputMask [
                            inputMask.mask "999-99-9999"
                            prop.onChange handleChange
                            prop.value ssn
                            inputMask.children (
                                fun (props) -> Mui.textField [
                                    textField.InputProps (props |> List.ofArray)
                                    textField.variant.outlined
                                    textField.label "SSN (editable)"
                                ]
                            )
                        ]
                    ]
                ]
            ]
        ]
        