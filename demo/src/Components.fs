namespace App

open Feliz
open Feliz.MaterialUI

type Components =
    /// <summary>
    /// The simplest possible React component.
    /// Shows a header with the text Hello World
    /// </summary>
    [<ReactComponent>]
    static member TestInputMask () =
        InputMask.inputMask [
            inputMask.mask "999-99-9999"
            inputMask.children (
                fun () -> Mui.textField [
                    textField.variant.outlined
                    textField.label "SSN"
                ]
            )
        ]