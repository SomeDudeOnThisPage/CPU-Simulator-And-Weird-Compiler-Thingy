# CPU-Simulator and weird compiler thingy
weird simulator of a scott-cpu but with a stack and 16 bit instead of 8 because I can add 128 + 75 in my head without a computer thanks for coming to my ted talk.

---

## Scott Stack Processor
This only includes one implementation of a processor-simulator, a random ass made up architecture
roughly based on the scott-CPU from the book 'But How Do It Know?'. This isn't cycle accurate or accurate
in any way anyway.

### Stack
Internally, a stack is used for most operations. The stack location in memory can be configured when
creating an instance of `ScottStackProcessor`. A word on the stack is always 16 bits in size, thus
requiring two reads from memory for each stack operation.

### Instructions
Instructions are always three bytes long, with an one-byte long opcode, as well as two parameter-bytes.
The instructions are roughly based on the instructions of the scott-CPU, but with
instructions having been changed either so that they use a stack instead of internal registers, or, in case their purpose
was to load data into a register (like `DATA`), completely removed.

| Instruction (byte 0) | Parameters (byte 1, byte 2) | Description                                                                                                 |
|----------------------|------------|-------------------------------------------------------------------------------------------------------------|
| `ADD` (0x80)         | -          | `ADD` pops 16-bit words (s0, s1) from the stack, performs `s0 + s1` and pushes the result onto the stack.   |
| `SHR` (0x90)         | -          | `SHR` pops 16-bit words (s0, s1) from the stack, performs `s0 >> s1` and pushes the result onto the stack.  |
| `SHL` (0xA0)         | -          | `SHL` pops 16-bit words (s0, s1) from the stack, performs `s0 << s1` and pushes the result onto the stack.  |
| `NOT` (0xB0)         | -          | `NOT` pops 16-bit word (s0) from the stack, performs `~s0` and pushes the result onto the stack.            |
| `AND` (0xC0)         | -          | `AND` pops 16-bit words (s0, s1) from the stack, performs `s0 & s1` and pushes the result onto the stack.   |
| `OR`  (0xD0)         | -          | `OR` pops 16-bit words (s0, s1) from the stack, performs `s0 \| s1` and pushes the result onto the stack.   |
| `XOR` (0xE0)         | -          | `XOR` pops 16-bit words (s0, s1) from the stack, performs `s0 ^ s1` and pushes the result onto the stack.   |
| `CMP` (0xF0)         | -          | `CMP` pops 16-bit words (s0, s1) from the stack, and sets CPU-Flags depending on the values. <ul><li>s0 = s1 &#8594; set E-Flag</li><li>s0 > s1 &#8594; set A-Flag</li><li>s0 = s1 = 0 &#8594; set Z-Flag</li></ul>               |
| `LD`  (0x00)         | -          | `LD` pops 16-bit word (s0) from the stack, reads the bytes at memory location `s0` and `s0 + 1`, and pushes them onto the stack. (The two bytes act as one single 16-bit word when used by any operation.)  |
| `ST`  (0x10)         | -          | `ST` pops 16-bit words (s0, s1) from the stack, and writes the lower nibble of `s1` to memory location `s0`, the higher nibble of `s1` to memory location `s0 + 1`                                          |
`// todo: the rest.`