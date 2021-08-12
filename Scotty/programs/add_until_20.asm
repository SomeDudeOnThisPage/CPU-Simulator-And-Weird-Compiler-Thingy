nop
nop
nop

.start
; clear all cpu flags
clf

; load byte at memory address 0x0DFF
spu 0x0DFF
ld

; load byte literal 1
spu 1

; add the bytes
add

; store byte at memory address 0x0DFF
spu 0x0DFF
st

; load byte from memory address 0x0DFF back // TODO: allow for storing without pop - so that we don't have to load again
spu 0x0DFF
ld

; load byte literal 25
spu 20

; compare 25 to word@0x0DFF
cmp

; jump back to start if a (s0) is larger b (s1)
; represented by "a"-flag in cpu, which is why the flags need to be cleared
ja .start

; terminate
duck
