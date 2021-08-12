; store byte literal 5 at memory address 0x0DFF
spu 5
spu 0xFF
spu 0x0D
st

; load byte from memory address 0x0DFF
spu 0xFF
spu 0x0D
ld

; load byte literal 2 onto the stack
spu 2

; add numbers
add

; store result at memory address 0x0DFD
spu 0xFD
spu 0x0D
st

; terminate
duck
