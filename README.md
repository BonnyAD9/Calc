# Calc
CLI Calculator

## Usage
```
calc <expression>
```

## Examples
```shell
> calc "5+5*5"
30
> calc "(5+5)*5"
50
> calc "asin(1)+sin^-1(1)"
3.141592653589793
> calc "log_3^-1(9)"
0.5
> calc "log_3(9)^-1"
-2
```
- the two last examples have different results because `^` has larger precedence than `log`
so `log_3^-1(9)` is the same as `(log_3(9))^-1`, however `log_3(9)^-1` is the same as `log_3(9^-1)`
- brackets in the last three examples are optional so `asin1+sin^-1 1`, `log_3^-1 9` and `log_3 9^-1` have the same meaning

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
- `atan^` - inverse tangens
- `sqrt` - square root
- `log_^` - logarithm (base 10 with no lower index)
- `ln^` - natural logarithm
- `lb^` - binary logarithm
- `sum^_` - sum

### Binary operators
- `+` - addition
- `-` - subtraction
- `*` - multiplication
- `*-` - multiplication by negation
- `/` - division
- `rt` - root (`2 rt a`=`sqrt(a)`)
- `^` - power
- `C` - combinations
- `P` - variations

### Postfix operators
- `!` - factorial

### Indexing
- `^` - upper index
- `_` - lower index

## Upper index behaviour
### Sum
- upper limit of the sum (if not present, `1000` is default)
- must be number or not present

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
### Sum
- lower limit of the sum (if not present, `i=1` is default)
+ `a=b` - b is number, a is name of variable to change in the sum
+ `a` - a is variable to change in the sum, lower limit is 1
+ `b` - b is number, the default variable name is i

### log
- `log_10(a)` = `log(a)`
- `log_e(a)` = `ln(a)`
- `log_2(a)` = `lb(a)`
- `log_b(a)` = logarithm of base b of a
