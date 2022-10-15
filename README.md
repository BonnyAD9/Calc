# Calc
CLI Calculator

## Usage
```
calc <expression>
```

## Support
- `_` after operator name means that it supports lower index
- `^` after operator name means that it supports upper index

### Brackets
- use brackets (`(` and `)`) to guide the parser
- use `[` and `]` for absolute value and to guide the parser

### Constants
- `pi` - 3.141592653589793
- `e` - 2.718281828459045

### Unary operators
- `-` - negation
- `abs` - absolute value
- `sin^` - sinus
- `cos^` - cosinus
- `tan^` - tangens
- `acos^` - inverse cosinus
- `asin^` - inverse sinus
- `atan^` - invers tangens
- `sqrt` - square root
- `log_^` - base 10 logarithm
- `ln^` - natural logarithm
- `lb^` - binary logarithm

### Binary operators
- `+` - addition
- `-` - subtraction
- `*` - multiplication
- `*-` - multiplication by negation
- `/` - division
- `rt` - root (2 rt a=sqrt(a))
- `^` - power

### Indexing
- `^` - upper index
- `_` - lower index

## Upper index behaviour
### Logarithmic functions
+ `log`, `ln`, `lg`
- `fun^1(a)` = `fun(a)`
- `fun^b(a)` = `(fun(a))^b`

### Goniometric functions
+  `sin`, `cos`, `tan`, `asin`, `acos`, `atan`
- `fun^1(a)` = `fun(a)`
- `fun^b(a)` = `(fun(a))^b`
- `fun^-1(a)` = `afun(a)`
- `afun^-1(a)` = `fun(a)`

## Lower index behaviour
### log
- `log_10(a)` = `log(a)`
- `log_e(a)` = `ln(a)`
- `log_2(a)` = `lb(a)`
- `log_b(a)` = logarithm of base b of a
