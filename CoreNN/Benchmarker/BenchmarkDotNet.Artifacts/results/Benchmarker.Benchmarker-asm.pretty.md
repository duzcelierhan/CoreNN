## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyAvx()
                   return DotMultiplyIntrinsicWAvx(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWAvx(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWAvx(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var span1 = vector1.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^
       xor     ebx,ebx
       xor     ebp,ebp
       mov     rcx,qword ptr [rsi]
       test    rcx,rcx
       je      M01_L02
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L00
       lea     rbx,[rcx+10h]
       mov     ebp,dword ptr [rcx+8]
       jmp     M01_L01
M01_L00:
       lea     rdx,[rsp+58h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rbx,qword ptr [rsp+58h]
       mov     ebp,dword ptr [rsp+60h]
M01_L01:
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       mov     eax,ecx
       add     rax,rdx
       mov     r8d,ebp
       cmp     rax,r8
       ja      M01_L17
       lea     rbx,[rbx+rdx*4]
       mov     ebp,ecx
                   var span2 = vector2.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       xor     r14d,r14d
       xor     r15d,r15d
       mov     rcx,qword ptr [rdi]
       test    rcx,rcx
       je      M01_L05
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L03
       lea     r14,[rcx+10h]
       mov     r15d,dword ptr [rcx+8]
       jmp     M01_L04
M01_L03:
       lea     rdx,[rsp+48h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     r14,qword ptr [rsp+48h]
       mov     r15d,dword ptr [rsp+50h]
M01_L04:
       mov     eax,dword ptr [rdi+8]
       and     eax,7FFFFFFFh
       mov     edx,dword ptr [rdi+0Ch]
       mov     ecx,edx
       add     rcx,rax
       mov     r8d,r15d
       cmp     rcx,r8
       ja      M01_L18
       lea     r14,[r14+rax*4]
       mov     r15d,edx
M01_L05:
       cmp     ebp,r15d
       jle     M01_L06
       mov     eax,r15d
       jmp     M01_L07
M01_L06:
       mov     eax,ebp
                   var v3 = Vector256.CreateScalarUnsafe(0f);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       vxorps  xmm0,xmm0,xmm0
                   var vectCnt = cnt / vectLen;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       sar     edx,3
                       for (i = 0; i < vectCnt; i++)
                            ^^^^^
       xor     ecx,ecx
                       for (i = 0; i < vectCnt; i++)
                                   ^^^^^^^^^^^
       test    edx,edx
       jle     M01_L09
                           var index = i * vectLen;
                           ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       mov     r8d,ecx
       shl     r8d,3
                           var v1 = Avx.LoadVector256((float*)Unsafe.AsPointer(ref span1[index]));
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,ebp
       jae     00007ffc`f8ee456b
       movsxd  r9,r8d
       lea     r9,[rbx+r9*4]
       vmovups ymm1,ymmword ptr [r9]
                           var v2 = Avx.LoadVector256((float*)Unsafe.AsPointer(ref span2[index]));
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,r15d
       jae     00007ffc`f8ee456b
       movsxd  r8,r8d
       lea     r8,[r14+r8*4]
       vmovups ymm2,ymmword ptr [r8]
                           var t = Avx.Multiply(v1, v2);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       vmulps  ymm1,ymm1,ymm2
                           v3 = Avx.Add(v3, t);
                           ^^^^^^^^^^^^^^^^^^^^
       vaddps  ymm0,ymm0,ymm1
                       for (i = 0; i < vectCnt; i++)
                                                ^^^
       inc     ecx
       cmp     ecx,edx
       jl      M01_L08
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
M01_L09:
       vxorps  xmm1,xmm1,xmm1
                   for (i = 0; i < vectLen; i++)
                        ^^^^^
       xor     ecx,ecx
                       total += v3.GetElement(i);
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       vmovupd ymmword ptr [rsp+20h],ymm0
       cmp     ecx,8
       jae     M01_L19
       lea     r8,[rsp+20h]
       movsxd  r9,ecx
       vmovss  xmm2,dword ptr [r8+r9*4]
       vaddss  xmm1,xmm1,xmm2
                   for (i = 0; i < vectLen; i++)
                                            ^^^
       inc     ecx
                   for (i = 0; i < vectLen; i++)
                               ^^^^^^^^^^^
       cmp     ecx,8
       jl      M01_L10
                   for (i = vectCnt * vectLen; i < cnt; i++)
                        ^^^^^^^^^^^^^^^^^^^^^
       mov     ecx,edx
       shl     ecx,3
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                               ^^^^^^^
       cmp     ecx,eax
       jge     M01_L12
                       total += span1[i] * span2[i];
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       cmp     ecx,ebp
       jae     00007ffc`f8ee456b
       movsxd  rdx,ecx
       vmovss  xmm0,dword ptr [rbx+rdx*4]
       cmp     ecx,r15d
       jae     00007ffc`f8ee456b
       movsxd  rdx,ecx
       vmulss  xmm0,xmm0,dword ptr [r14+rdx*4]
       vaddss  xmm0,xmm0,xmm1
       vmovaps xmm1,xmm0
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                                        ^^^
       inc     ecx
       cmp     ecx,eax
       jl      M01_L11
M01_L12:
       mov     edx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rdi+0Ch]
       je      M01_L16
       mov     edx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rdi+0Ch]
       jg      M01_L13
                         for (var j = cnt; j < h.Length; j++)
                              ^^^^^^^^^^^
       jmp     M01_L14
M01_L13:
       mov     r14,rbx
       mov     r15d,ebp
M01_L14:
       cmp     eax,r15d
       jge     M01_L16
                           total += h[j];
                           ^^^^^^^^^^^^^^
M01_L15:
       cmp     eax,r15d
       jae     00007ffc`f8ee456b
       movsxd  rdx,eax
       vaddss  xmm1,xmm1,dword ptr [r14+rdx*4]
                         for (var j = cnt; j < h.Length; j++)
                                                         ^^^
       inc     eax
       cmp     eax,r15d
       jl      M01_L15
                   return total;
                   ^^^^^^^^^^^^^
M01_L16:
       vmovaps xmm0,xmm1
       vzeroupper
       add     rsp,68h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r14
       pop     r15
       ret
M01_L17:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L18:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L19:
       mov     ecx,15h
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       int     3
; Total bytes of code 480
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyAvxWSpanPtr()
                   return DotMultiplyIntrinsicWAvxWSpanPtr(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWAvxWSpanPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWAvxWSpanPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var span1 = vector1.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^
       xor     ebx,ebx
       xor     ebp,ebp
       mov     rcx,qword ptr [rsi]
       test    rcx,rcx
       je      M01_L02
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L00
       lea     rbx,[rcx+10h]
       mov     ebp,dword ptr [rcx+8]
       jmp     M01_L01
M01_L00:
       lea     rdx,[rsp+58h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rbx,qword ptr [rsp+58h]
       mov     ebp,dword ptr [rsp+60h]
M01_L01:
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       mov     eax,ecx
       add     rax,rdx
       mov     r8d,ebp
       cmp     rax,r8
       ja      M01_L17
       lea     rbx,[rbx+rdx*4]
       mov     ebp,ecx
                   var span2 = vector2.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       xor     r14d,r14d
       xor     r15d,r15d
       mov     rcx,qword ptr [rdi]
       test    rcx,rcx
       je      M01_L05
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L03
       lea     r14,[rcx+10h]
       mov     r15d,dword ptr [rcx+8]
       jmp     M01_L04
M01_L03:
       lea     rdx,[rsp+48h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     r14,qword ptr [rsp+48h]
       mov     r15d,dword ptr [rsp+50h]
M01_L04:
       mov     eax,dword ptr [rdi+8]
       and     eax,7FFFFFFFh
       mov     edx,dword ptr [rdi+0Ch]
       mov     ecx,edx
       add     rcx,rax
       mov     r8d,r15d
       cmp     rcx,r8
       ja      M01_L18
       lea     r14,[r14+rax*4]
       mov     r15d,edx
M01_L05:
       cmp     ebp,r15d
       jle     M01_L06
       mov     eax,r15d
       jmp     M01_L07
M01_L06:
       mov     eax,ebp
                   var v3 = Vector256.CreateScalarUnsafe(0f);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       vxorps  xmm0,xmm0,xmm0
                   var vectCnt = cnt / vectLen;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       sar     edx,3
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
       vxorps  xmm1,xmm1,xmm1
                       var ptr1 = (float*)Unsafe.AsPointer(ref span1[0]);
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     ebp,0
       jbe     00007ffc`f8ed457e
       mov     rcx,rbx
                       var ptr2 = (float*)Unsafe.AsPointer(ref span2[0]);
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r15d,0
       jbe     00007ffc`f8ed457e
       mov     r8,r14
                       for (i = 0; i < vectCnt; i++)
                            ^^^^^
       xor     r9d,r9d
                       for (i = 0; i < vectCnt; i++)
                                   ^^^^^^^^^^^
       test    edx,edx
       jle     M01_L09
                           var v1 = Avx.LoadVector256(ptr1);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       vmovups ymm2,ymmword ptr [rcx]
                           var v2 = Avx.LoadVector256(ptr2);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       vmovups ymm3,ymmword ptr [r8]
                           var t = Avx.Multiply(v1, v2);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       vmulps  ymm2,ymm2,ymm3
                           v3 = Avx.Add(v3, t);
                           ^^^^^^^^^^^^^^^^^^^^
       vaddps  ymm0,ymm0,ymm2
                           ptr1 += vectLen;
                           ^^^^^^^^^^^^^^^^
       add     rcx,20h
                           ptr2 += vectLen;
                           ^^^^^^^^^^^^^^^^
       add     r8,20h
                       for (i = 0; i < vectCnt; i++)
                                                ^^^
       inc     r9d
       cmp     r9d,edx
       jl      M01_L08
                       for (i = 0; i < vectLen; i++)
                            ^^^^^
M01_L09:
       xor     r9d,r9d
                           total += v3.GetElement(i);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       vmovupd ymmword ptr [rsp+20h],ymm0
       cmp     r9d,8
       jae     M01_L19
       lea     rcx,[rsp+20h]
       movsxd  r8,r9d
       vmovss  xmm2,dword ptr [rcx+r8*4]
       vaddss  xmm1,xmm1,xmm2
                       for (i = 0; i < vectLen; i++)
                                                ^^^
       inc     r9d
                       for (i = 0; i < vectLen; i++)
                                   ^^^^^^^^^^^
       cmp     r9d,8
       jl      M01_L10
                       i = vectCnt * vectLen;
                       ^^^^^^^^^^^^^^^^^^^^^^
       mov     r9d,edx
       shl     r9d,3
                       if (cnt % vectLen > 0)
                       ^^^^^^^^^^^^^^^^^^^^^^
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       and     edx,0FFFFFFF8h
       mov     ecx,eax
       sub     ecx,edx
       test    ecx,ecx
       jle     M01_L12
                           ptr1 = (float*)Unsafe.AsPointer(ref span1[i]);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r9d,ebp
       jae     00007ffc`f8ed457e
       movsxd  rdx,r9d
       lea     rcx,[rbx+rdx*4]
                           ptr2 = (float*)Unsafe.AsPointer(ref span2[i]);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r9d,r15d
       jae     00007ffc`f8ed457e
       movsxd  rdx,r9d
       lea     r8,[r14+rdx*4]
                           for (; i < cnt; i++)
                                  ^^^^^^^
       cmp     r9d,eax
       jge     M01_L12
                               total += *ptr1++ * *ptr2++;
                               ^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       mov     rdx,rcx
       lea     rcx,[rdx+4]
       mov     r10,r8
       lea     r8,[r10+4]
       vmovss  xmm0,dword ptr [rdx]
       vmulss  xmm0,xmm0,dword ptr [r10]
       vaddss  xmm0,xmm0,xmm1
       vmovaps xmm1,xmm0
                           for (; i < cnt; i++)
                                           ^^^
       inc     r9d
       cmp     r9d,eax
       jl      M01_L11
M01_L12:
       mov     edx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rdi+0Ch]
       je      M01_L16
       mov     edx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rdi+0Ch]
       jg      M01_L13
                         for (var j = cnt; j < h.Length; j++)
                              ^^^^^^^^^^^
       jmp     M01_L14
M01_L13:
       mov     r14,rbx
       mov     r15d,ebp
M01_L14:
       cmp     eax,r15d
       jge     M01_L16
                           total += h[j];
                           ^^^^^^^^^^^^^^
M01_L15:
       cmp     eax,r15d
       jae     00007ffc`f8ed457e
       movsxd  rdx,eax
       vaddss  xmm1,xmm1,dword ptr [r14+rdx*4]
                         for (var j = cnt; j < h.Length; j++)
                                                         ^^^
       inc     eax
       cmp     eax,r15d
       jl      M01_L15
                   return total;
                   ^^^^^^^^^^^^^
M01_L16:
       vmovaps xmm0,xmm1
       vzeroupper
       add     rsp,68h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r14
       pop     r15
       ret
M01_L17:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L18:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L19:
       mov     ecx,15h
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       int     3
; Total bytes of code 531
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyFma()
                   return DotMultiplyIntrinsicWFma(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWFma(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWFma(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var span1 = mem1.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^
       xor     ebx,ebx
       xor     ebp,ebp
       mov     rcx,qword ptr [rsi]
       test    rcx,rcx
       je      M01_L02
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L00
       lea     rbx,[rcx+10h]
       mov     ebp,dword ptr [rcx+8]
       jmp     M01_L01
M01_L00:
       lea     rdx,[rsp+60h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rbx,qword ptr [rsp+60h]
       mov     ebp,dword ptr [rsp+68h]
M01_L01:
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       mov     eax,ecx
       add     rax,rdx
       mov     r8d,ebp
       cmp     rax,r8
       ja      M01_L17
       lea     rbx,[rbx+rdx*4]
       mov     ebp,ecx
                   var span2 = mem2.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       xor     esi,esi
       xor     r14d,r14d
       mov     rcx,qword ptr [rdi]
       test    rcx,rcx
       je      M01_L05
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L03
       lea     rsi,[rcx+10h]
       mov     r14d,dword ptr [rcx+8]
       jmp     M01_L04
M01_L03:
       lea     rdx,[rsp+50h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rsi,qword ptr [rsp+50h]
       mov     r14d,dword ptr [rsp+58h]
M01_L04:
       mov     eax,dword ptr [rdi+8]
       and     eax,7FFFFFFFh
       mov     edx,dword ptr [rdi+0Ch]
       mov     ecx,edx
       add     rcx,rax
       mov     r8d,r14d
       cmp     rcx,r8
       ja      M01_L18
       lea     rsi,[rsi+rax*4]
       mov     r14d,edx
M01_L05:
       cmp     ebp,r14d
       jle     M01_L06
       mov     eax,r14d
       jmp     M01_L07
M01_L06:
       mov     eax,ebp
                   var v3 = Vector256.CreateScalarUnsafe(0f);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       vxorps  xmm0,xmm0,xmm0
                   var vectCnt = cnt / vectLen;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       sar     edx,3
                       for (i = 0; i < vectCnt; i++)
                            ^^^^^
       xor     ecx,ecx
                       for (i = 0; i < vectCnt; i++)
                                   ^^^^^^^^^^^
       test    edx,edx
       jle     M01_L09
                           var index = i * vectLen;
                           ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       mov     r8d,ecx
       shl     r8d,3
                           var v1 = Avx.LoadVector256((float*) Unsafe.AsPointer(ref span1[index]));
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,ebp
       jae     00007ffc`f8ed494f
       movsxd  r9,r8d
       lea     r9,[rbx+r9*4]
       vmovups ymm1,ymmword ptr [r9]
                           var v2 = Avx.LoadVector256((float*) Unsafe.AsPointer(ref span2[index]));
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,r14d
       jae     00007ffc`f8ed494f
       movsxd  r8,r8d
       lea     r8,[rsi+r8*4]
       vmovups ymm2,ymmword ptr [r8]
                           v3 = Fma.MultiplyAdd(v1, v2, v3);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       vfmadd213ps ymm1,ymm2,ymm0
       vmovaps ymm0,ymm1
                       for (i = 0; i < vectCnt; i++)
                                                ^^^
       inc     ecx
       cmp     ecx,edx
       jl      M01_L08
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
M01_L09:
       vxorps  xmm1,xmm1,xmm1
                   for (i = 0; i < vectLen; i++)
                        ^^^^^
       xor     ecx,ecx
                       total += v3.GetElement(i);
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       vmovupd ymmword ptr [rsp+20h],ymm0
       cmp     ecx,8
       jae     M01_L19
       lea     r8,[rsp+20h]
       movsxd  r9,ecx
       vmovss  xmm2,dword ptr [r8+r9*4]
       vaddss  xmm1,xmm1,xmm2
                   for (i = 0; i < vectLen; i++)
                                            ^^^
       inc     ecx
                   for (i = 0; i < vectLen; i++)
                               ^^^^^^^^^^^
       cmp     ecx,8
       jl      M01_L10
                   for (i = vectCnt * vectLen; i < cnt; i++)
                        ^^^^^^^^^^^^^^^^^^^^^
       mov     ecx,edx
       shl     ecx,3
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                               ^^^^^^^
       cmp     ecx,eax
       jge     M01_L12
                       total += span1[i] * span2[i];
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       cmp     ecx,ebp
       jae     00007ffc`f8ed494f
       movsxd  rdx,ecx
       vmovss  xmm0,dword ptr [rbx+rdx*4]
       cmp     ecx,r14d
       jae     00007ffc`f8ed494f
       movsxd  rdx,ecx
       vmulss  xmm0,xmm0,dword ptr [rsi+rdx*4]
       vaddss  xmm0,xmm0,xmm1
       vmovaps xmm1,xmm0
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                                        ^^^
       inc     ecx
       cmp     ecx,eax
       jl      M01_L11
M01_L12:
       cmp     ebp,r14d
       je      M01_L16
       cmp     ebp,r14d
       jg      M01_L13
                         for (var j = cnt; j < h.Length; j++)
                              ^^^^^^^^^^^
       jmp     M01_L14
M01_L13:
       mov     rsi,rbx
       mov     r14d,ebp
M01_L14:
       cmp     eax,r14d
       jge     M01_L16
                             total += h[j];
                             ^^^^^^^^^^^^^^
M01_L15:
       cmp     eax,r14d
       jae     00007ffc`f8ed494f
       movsxd  rdx,eax
       vaddss  xmm1,xmm1,dword ptr [rsi+rdx*4]
                         for (var j = cnt; j < h.Length; j++)
                                                         ^^^
       inc     eax
       cmp     eax,r14d
       jl      M01_L15
                   return total;
                   ^^^^^^^^^^^^^
M01_L16:
       vmovaps xmm0,xmm1
       vzeroupper
       add     rsp,70h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r14
       ret
M01_L17:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L18:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L19:
       mov     ecx,15h
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       int     3
; Total bytes of code 470
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyFmaWSpanPtr()
                   return DotMultiplyIntrinsicWFmaWSpanPtr(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWFmaWSpanPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWFmaWSpanPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var span1 = vector1.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^
       xor     ebx,ebx
       xor     ebp,ebp
       mov     rcx,qword ptr [rsi]
       test    rcx,rcx
       je      M01_L02
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L00
       lea     rbx,[rcx+10h]
       mov     ebp,dword ptr [rcx+8]
       jmp     M01_L01
M01_L00:
       lea     rdx,[rsp+58h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rbx,qword ptr [rsp+58h]
       mov     ebp,dword ptr [rsp+60h]
M01_L01:
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       mov     eax,ecx
       add     rax,rdx
       mov     r8d,ebp
       cmp     rax,r8
       ja      M01_L17
       lea     rbx,[rbx+rdx*4]
       mov     ebp,ecx
                   var span2 = vector2.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       xor     r14d,r14d
       xor     r15d,r15d
       mov     rcx,qword ptr [rdi]
       test    rcx,rcx
       je      M01_L05
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L03
       lea     r14,[rcx+10h]
       mov     r15d,dword ptr [rcx+8]
       jmp     M01_L04
M01_L03:
       lea     rdx,[rsp+48h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     r14,qword ptr [rsp+48h]
       mov     r15d,dword ptr [rsp+50h]
M01_L04:
       mov     eax,dword ptr [rdi+8]
       and     eax,7FFFFFFFh
       mov     edx,dword ptr [rdi+0Ch]
       mov     ecx,edx
       add     rcx,rax
       mov     r8d,r15d
       cmp     rcx,r8
       ja      M01_L18
       lea     r14,[r14+rax*4]
       mov     r15d,edx
M01_L05:
       cmp     ebp,r15d
       jle     M01_L06
       mov     eax,r15d
       jmp     M01_L07
M01_L06:
       mov     eax,ebp
                   var v3 = Vector256.CreateScalarUnsafe(0f);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       vxorps  xmm0,xmm0,xmm0
                   var vectCnt = cnt / vectLen;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       sar     edx,3
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
       vxorps  xmm1,xmm1,xmm1
                       var ptr1 = (float*) Unsafe.AsPointer(ref span1[0]);
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     ebp,0
       jbe     00007ffc`f8ee499f
       mov     rcx,rbx
                       var ptr2 = (float*) Unsafe.AsPointer(ref span2[0]);
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r15d,0
       jbe     00007ffc`f8ee499f
       mov     r8,r14
                       for (i = 0; i < vectCnt; i++)
                            ^^^^^
       xor     r9d,r9d
                       for (i = 0; i < vectCnt; i++)
                                   ^^^^^^^^^^^
       test    edx,edx
       jle     M01_L09
                           var v1 = Avx.LoadVector256(ptr1);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       vmovups ymm2,ymmword ptr [rcx]
                           var v2 = Avx.LoadVector256(ptr2);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       vmovups ymm3,ymmword ptr [r8]
                           v3 = Fma.MultiplyAdd(v1, v2, v3);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       vfmadd213ps ymm2,ymm3,ymm0
       vmovaps ymm0,ymm2
                           ptr1 += vectLen;
                           ^^^^^^^^^^^^^^^^
       add     rcx,20h
                           ptr2 += vectLen;
                           ^^^^^^^^^^^^^^^^
       add     r8,20h
                       for (i = 0; i < vectCnt; i++)
                                                ^^^
       inc     r9d
       cmp     r9d,edx
       jl      M01_L08
                       for (i = 0; i < vectLen; i++)
                            ^^^^^
M01_L09:
       xor     r9d,r9d
                           total += v3.GetElement(i);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       vmovupd ymmword ptr [rsp+20h],ymm0
       cmp     r9d,8
       jae     M01_L19
       lea     rcx,[rsp+20h]
       movsxd  r8,r9d
       vmovss  xmm2,dword ptr [rcx+r8*4]
       vaddss  xmm1,xmm1,xmm2
                       for (i = 0; i < vectLen; i++)
                                                ^^^
       inc     r9d
                       for (i = 0; i < vectLen; i++)
                                   ^^^^^^^^^^^
       cmp     r9d,8
       jl      M01_L10
                       i = vectCnt * vectLen;
                       ^^^^^^^^^^^^^^^^^^^^^^
       mov     r9d,edx
       shl     r9d,3
                       if (cnt % vectLen > 0)
                       ^^^^^^^^^^^^^^^^^^^^^^
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       and     edx,0FFFFFFF8h
       mov     ecx,eax
       sub     ecx,edx
       test    ecx,ecx
       jle     M01_L12
                           ptr1 = (float*) Unsafe.AsPointer(ref span1[i]);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r9d,ebp
       jae     00007ffc`f8ee499f
       movsxd  rdx,r9d
       lea     rcx,[rbx+rdx*4]
                           ptr2 = (float*) Unsafe.AsPointer(ref span2[i]);
                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r9d,r15d
       jae     00007ffc`f8ee499f
       movsxd  rdx,r9d
       lea     r8,[r14+rdx*4]
                           for (; i < cnt; i++)
                                  ^^^^^^^
       cmp     r9d,eax
       jge     M01_L12
                               total += *ptr1++ * *ptr2++;
                               ^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       mov     rdx,rcx
       lea     rcx,[rdx+4]
       mov     r10,r8
       lea     r8,[r10+4]
       vmovss  xmm0,dword ptr [rdx]
       vmulss  xmm0,xmm0,dword ptr [r10]
       vaddss  xmm0,xmm0,xmm1
       vmovaps xmm1,xmm0
                           for (; i < cnt; i++)
                                           ^^^
       inc     r9d
       cmp     r9d,eax
       jl      M01_L11
M01_L12:
       mov     edx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rdi+0Ch]
       je      M01_L16
       mov     edx,dword ptr [rsi+0Ch]
       cmp     edx,dword ptr [rdi+0Ch]
       jg      M01_L13
                         for (var j = cnt; j < h.Length; j++)
                              ^^^^^^^^^^^
       jmp     M01_L14
M01_L13:
       mov     r14,rbx
       mov     r15d,ebp
M01_L14:
       cmp     eax,r15d
       jge     M01_L16
                           total += h[j];
                           ^^^^^^^^^^^^^^
M01_L15:
       cmp     eax,r15d
       jae     00007ffc`f8ee499f
       movsxd  rdx,eax
       vaddss  xmm1,xmm1,dword ptr [r14+rdx*4]
                         for (var j = cnt; j < h.Length; j++)
                                                         ^^^
       inc     eax
       cmp     eax,r15d
       jl      M01_L15
                   return total;
                   ^^^^^^^^^^^^^
M01_L16:
       vmovaps xmm0,xmm1
       vzeroupper
       add     rsp,68h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r14
       pop     r15
       ret
M01_L17:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L18:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L19:
       mov     ecx,15h
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       int     3
; Total bytes of code 532
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyWithVector()
                   return DotMultiplyIntrinsicWVector(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVector(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVector(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var span1 = mem1.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^
       xor     ebx,ebx
       xor     ebp,ebp
       mov     rcx,qword ptr [rsi]
       test    rcx,rcx
       je      M01_L02
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L00
       lea     rbx,[rcx+10h]
       mov     ebp,dword ptr [rcx+8]
       jmp     M01_L01
M01_L00:
       lea     rdx,[rsp+50h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rbx,qword ptr [rsp+50h]
       mov     ebp,dword ptr [rsp+58h]
M01_L01:
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       mov     eax,ecx
       add     rax,rdx
       mov     r8d,ebp
       cmp     rax,r8
       ja      M01_L17
       lea     rbx,[rbx+rdx*4]
       mov     ebp,ecx
                   var span2 = mem2.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       xor     esi,esi
       xor     r14d,r14d
       mov     rcx,qword ptr [rdi]
       test    rcx,rcx
       je      M01_L05
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L03
       lea     rsi,[rcx+10h]
       mov     r14d,dword ptr [rcx+8]
       jmp     M01_L04
M01_L03:
       lea     rdx,[rsp+40h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rsi,qword ptr [rsp+40h]
       mov     r14d,dword ptr [rsp+48h]
M01_L04:
       mov     eax,dword ptr [rdi+8]
       and     eax,7FFFFFFFh
       mov     edx,dword ptr [rdi+0Ch]
       mov     ecx,edx
       add     rcx,rax
       mov     r8d,r14d
       cmp     rcx,r8
       ja      M01_L18
       lea     rsi,[rsi+rax*4]
       mov     r14d,edx
M01_L05:
       cmp     ebp,r14d
       jle     M01_L06
       mov     eax,r14d
       jmp     M01_L07
M01_L06:
       mov     eax,ebp
                   var v3 = Vector<float>.Zero;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       vxorps  ymm0,ymm0,ymm0
                   var vectCnt = cnt / vectLen;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       sar     edx,3
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
       vxorps  xmm1,xmm1,xmm1
                   for (i = 0; i < vectCnt; i++)
                        ^^^^^
       xor     ecx,ecx
                   for (i = 0; i < vectCnt; i++)
                               ^^^^^^^^^^^
       test    edx,edx
       jle     M01_L09
                       var index = i * vectLen;
                       ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       mov     r8d,ecx
       shl     r8d,3
                       var v1 = new Vector<float>(span1.Slice(index));
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,ebp
       ja      M01_L19
       mov     r9d,ebp
       sub     r9d,r8d
       movsxd  r10,r8d
       lea     r10,[rbx+r10*4]
       cmp     r9d,8
       jl      M01_L20
       vmovupd ymm2,ymmword ptr [r10]
                       var v2 = new Vector<float>(span2.Slice(index));
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,r14d
       ja      M01_L21
       mov     r9d,r14d
       sub     r9d,r8d
       movsxd  r8,r8d
       lea     r8,[rsi+r8*4]
       cmp     r9d,8
       jl      00007ffc`f8eb496b
       vmovupd ymm3,ymmword ptr [r8]
                       v3 += v1 * v2;
                       ^^^^^^^^^^^^^^
       vmulps  ymm2,ymm2,ymm3
       vaddps  ymm0,ymm0,ymm2
                   for (i = 0; i < vectCnt; i++)
                                            ^^^
       inc     ecx
       cmp     ecx,edx
       jl      M01_L08
                   for (i = 0; i < vectLen; i++)
                        ^^^^^
M01_L09:
       xor     ecx,ecx
                       total += v3[i];
                       ^^^^^^^^^^^^^^^
M01_L10:
       vmovupd ymmword ptr [rsp+20h],ymm0
       vmovss  xmm2,dword ptr [rsp+rcx*4+20h]
       vaddss  xmm1,xmm1,xmm2
                   for (i = 0; i < vectLen; i++)
                                            ^^^
       inc     ecx
                   for (i = 0; i < vectLen; i++)
                               ^^^^^^^^^^^
       cmp     ecx,8
       jl      M01_L10
                   for (i = vectCnt * vectLen; i < cnt; i++)
                        ^^^^^^^^^^^^^^^^^^^^^
       mov     ecx,edx
       shl     ecx,3
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                               ^^^^^^^
       cmp     ecx,eax
       jge     M01_L12
                       total += span1[i] * span2[i];
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       cmp     ecx,ebp
       jae     00007ffc`f8eb4976
       movsxd  rdx,ecx
       vmovss  xmm0,dword ptr [rbx+rdx*4]
       cmp     ecx,r14d
       jae     00007ffc`f8eb4976
       movsxd  rdx,ecx
       vmulss  xmm0,xmm0,dword ptr [rsi+rdx*4]
       vaddss  xmm0,xmm0,xmm1
       vmovaps xmm1,xmm0
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                                        ^^^
       inc     ecx
       cmp     ecx,eax
       jl      M01_L11
M01_L12:
       cmp     ebp,r14d
       je      M01_L16
       cmp     ebp,r14d
       jg      M01_L13
                       for (var j = cnt; j < h.Length; j++)
                            ^^^^^^^^^^^
       jmp     M01_L14
M01_L13:
       mov     rsi,rbx
       mov     r14d,ebp
M01_L14:
       cmp     eax,r14d
       jge     M01_L16
                           total += h[j];
                           ^^^^^^^^^^^^^^
M01_L15:
       cmp     eax,r14d
       jae     00007ffc`f8eb4976
       movsxd  rdx,eax
       vaddss  xmm1,xmm1,dword ptr [rsi+rdx*4]
                       for (var j = cnt; j < h.Length; j++)
                                                       ^^^
       inc     eax
       cmp     eax,r14d
       jl      M01_L15
                   return total;
                   ^^^^^^^^^^^^^
M01_L16:
       vmovaps xmm0,xmm1
       vzeroupper
       add     rsp,60h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r14
       ret
M01_L17:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L18:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L19:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L20:
       mov     ecx,8
       call    System.Numerics.Vector.ThrowInsufficientNumberOfElementsException(Int32)
       int     3
M01_L21:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
; Total bytes of code 500
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyWithVectorDot()
                   return DotMultiplyIntrinsicWVectorDot(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVectorDot(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVectorDot(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var span1 = mem1.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^
       xor     ebx,ebx
       xor     ebp,ebp
       mov     rcx,qword ptr [rsi]
       test    rcx,rcx
       je      M01_L02
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L00
       lea     rbx,[rcx+10h]
       mov     ebp,dword ptr [rcx+8]
       jmp     M01_L01
M01_L00:
       lea     rdx,[rsp+30h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rbx,qword ptr [rsp+30h]
       mov     ebp,dword ptr [rsp+38h]
M01_L01:
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       mov     eax,ecx
       add     rax,rdx
       mov     r8d,ebp
       cmp     rax,r8
       ja      M01_L16
       lea     rbx,[rbx+rdx*4]
       mov     ebp,ecx
                   var span2 = mem2.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       xor     esi,esi
       xor     r14d,r14d
       mov     rcx,qword ptr [rdi]
       test    rcx,rcx
       je      M01_L05
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L03
       lea     rsi,[rcx+10h]
       mov     r14d,dword ptr [rcx+8]
       jmp     M01_L04
M01_L03:
       lea     rdx,[rsp+20h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rsi,qword ptr [rsp+20h]
       mov     r14d,dword ptr [rsp+28h]
M01_L04:
       mov     eax,dword ptr [rdi+8]
       and     eax,7FFFFFFFh
       mov     edx,dword ptr [rdi+0Ch]
       mov     ecx,edx
       add     rcx,rax
       mov     r8d,r14d
       cmp     rcx,r8
       ja      M01_L17
       lea     rsi,[rsi+rax*4]
       mov     r14d,edx
M01_L05:
       cmp     ebp,r14d
       jle     M01_L06
       mov     eax,r14d
       jmp     M01_L07
M01_L06:
       mov     eax,ebp
                   var vectLen = Vector<float>.Count;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       sar     edx,3
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
       vxorps  xmm0,xmm0,xmm0
                   for (i = 0; i < vectCnt; i++)
                        ^^^^^
       xor     ecx,ecx
                   for (i = 0; i < vectCnt; i++)
                               ^^^^^^^^^^^
       test    edx,edx
       jle     M01_L09
                       var index = i * vectLen;
                       ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       mov     r8d,ecx
       shl     r8d,3
                       var v1 = new Vector<float>(span1.Slice(index));
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,ebp
       ja      M01_L18
       mov     r9d,ebp
       sub     r9d,r8d
       movsxd  r10,r8d
       lea     r10,[rbx+r10*4]
       cmp     r9d,8
       jl      M01_L19
       vmovupd ymm1,ymmword ptr [r10]
                       var v2 = new Vector<float>(span2.Slice(index));
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,r14d
       ja      M01_L20
       mov     r9d,r14d
       sub     r9d,r8d
       movsxd  r8,r8d
       lea     r8,[rsi+r8*4]
       cmp     r9d,8
       jl      00007ffc`f8eb4958
       vmovupd ymm2,ymmword ptr [r8]
                       total += Vector.Dot(v1, v2);
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       vdpps   ymm1,ymm1,ymm2,0F1h
       vextractf128 xmm3,ymm1,1
       vaddps  xmm1,xmm1,xmm3
       vaddss  xmm0,xmm0,xmm1
                   for (i = 0; i < vectCnt; i++)
                                            ^^^
       inc     ecx
       cmp     ecx,edx
       jl      M01_L08
                   for (i = vectCnt * vectLen; i < cnt; i++)
                        ^^^^^^^^^^^^^^^^^^^^^
M01_L09:
       mov     ecx,edx
       shl     ecx,3
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                               ^^^^^^^
       cmp     ecx,eax
       jge     M01_L11
                       total += span1[i] * span2[i];
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       cmp     ecx,ebp
       jae     00007ffc`f8eb4963
       movsxd  rdx,ecx
       vmovss  xmm1,dword ptr [rbx+rdx*4]
       cmp     ecx,r14d
       jae     00007ffc`f8eb4963
       movsxd  rdx,ecx
       vmulss  xmm1,xmm1,dword ptr [rsi+rdx*4]
       vaddss  xmm1,xmm1,xmm0
       vmovaps xmm0,xmm1
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                                        ^^^
       inc     ecx
       cmp     ecx,eax
       jl      M01_L10
M01_L11:
       cmp     ebp,r14d
       je      M01_L15
       cmp     ebp,r14d
       jg      M01_L12
                       for (var j = cnt; j < h.Length; j++)
                            ^^^^^^^^^^^
       jmp     M01_L13
M01_L12:
       mov     rsi,rbx
       mov     r14d,ebp
M01_L13:
       cmp     eax,r14d
       jge     M01_L15
                           total += h[j];
                           ^^^^^^^^^^^^^^
M01_L14:
       cmp     eax,r14d
       jae     00007ffc`f8eb4963
       movsxd  rdx,eax
       vaddss  xmm0,xmm0,dword ptr [rsi+rdx*4]
                       for (var j = cnt; j < h.Length; j++)
                                                       ^^^
       inc     eax
       cmp     eax,r14d
       jl      M01_L14
                   return total;
                   ^^^^^^^^^^^^^
M01_L15:
       vzeroupper
       add     rsp,40h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r14
       ret
M01_L16:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L17:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L18:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L19:
       mov     ecx,8
       call    System.Numerics.Vector.ThrowInsufficientNumberOfElementsException(Int32)
       int     3
M01_L20:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
; Total bytes of code 479
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyWithVectorMul()
                   return DotMultiplyIntrinsicWVectorMul(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVectorMul(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVectorMul(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var span1 = mem1.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^
       xor     ebx,ebx
       xor     ebp,ebp
       mov     rcx,qword ptr [rsi]
       test    rcx,rcx
       je      M01_L02
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L00
       lea     rbx,[rcx+10h]
       mov     ebp,dword ptr [rcx+8]
       jmp     M01_L01
M01_L00:
       lea     rdx,[rsp+50h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rbx,qword ptr [rsp+50h]
       mov     ebp,dword ptr [rsp+58h]
M01_L01:
       mov     edx,dword ptr [rsi+8]
       and     edx,7FFFFFFFh
       mov     ecx,dword ptr [rsi+0Ch]
       mov     eax,ecx
       add     rax,rdx
       mov     r8d,ebp
       cmp     rax,r8
       ja      M01_L17
       lea     rbx,[rbx+rdx*4]
       mov     ebp,ecx
                   var span2 = mem2.Span;
                   ^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       xor     esi,esi
       xor     r14d,r14d
       mov     rcx,qword ptr [rdi]
       test    rcx,rcx
       je      M01_L05
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L03
       lea     rsi,[rcx+10h]
       mov     r14d,dword ptr [rcx+8]
       jmp     M01_L04
M01_L03:
       lea     rdx,[rsp+40h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rsi,qword ptr [rsp+40h]
       mov     r14d,dword ptr [rsp+48h]
M01_L04:
       mov     eax,dword ptr [rdi+8]
       and     eax,7FFFFFFFh
       mov     edx,dword ptr [rdi+0Ch]
       mov     ecx,edx
       add     rcx,rax
       mov     r8d,r14d
       cmp     rcx,r8
       ja      M01_L18
       lea     rsi,[rsi+rax*4]
       mov     r14d,edx
M01_L05:
       cmp     ebp,r14d
       jle     M01_L06
       mov     eax,r14d
       jmp     M01_L07
M01_L06:
       mov     eax,ebp
                   var v3 = Vector<float>.Zero;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       vxorps  ymm0,ymm0,ymm0
                   var vectCnt = cnt / vectLen;
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     edx,eax
       sar     edx,1Fh
       and     edx,7
       add     edx,eax
       sar     edx,3
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
       vxorps  xmm1,xmm1,xmm1
                   for (i = 0; i < vectCnt; i++)
                        ^^^^^
       xor     ecx,ecx
                   for (i = 0; i < vectCnt; i++)
                               ^^^^^^^^^^^
       test    edx,edx
       jle     M01_L09
                       var index = i * vectLen;
                       ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       mov     r8d,ecx
       shl     r8d,3
                       var v1 = new Vector<float>(span1.Slice(index));
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,ebp
       ja      M01_L19
       mov     r9d,ebp
       sub     r9d,r8d
       movsxd  r10,r8d
       lea     r10,[rbx+r10*4]
       cmp     r9d,8
       jl      M01_L20
       vmovupd ymm2,ymmword ptr [r10]
                       var v2 = new Vector<float>(span2.Slice(index));
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       cmp     r8d,r14d
       ja      M01_L21
       mov     r9d,r14d
       sub     r9d,r8d
       movsxd  r8,r8d
       lea     r8,[rsi+r8*4]
       cmp     r9d,8
       jl      00007ffc`f8ed4bcb
       vmovupd ymm3,ymmword ptr [r8]
       vmulps  ymm2,ymm2,ymm3
       vaddps  ymm0,ymm0,ymm2
                   for (i = 0; i < vectCnt; i++)
                                            ^^^
       inc     ecx
       cmp     ecx,edx
       jl      M01_L08
                   for (i = 0; i < vectLen; i++)
                        ^^^^^
M01_L09:
       xor     ecx,ecx
                       total += v3[i];
                       ^^^^^^^^^^^^^^^
M01_L10:
       vmovupd ymmword ptr [rsp+20h],ymm0
       vmovss  xmm2,dword ptr [rsp+rcx*4+20h]
       vaddss  xmm1,xmm1,xmm2
                   for (i = 0; i < vectLen; i++)
                                            ^^^
       inc     ecx
                   for (i = 0; i < vectLen; i++)
                               ^^^^^^^^^^^
       cmp     ecx,8
       jl      M01_L10
                   for (i = vectCnt * vectLen; i < cnt; i++)
                        ^^^^^^^^^^^^^^^^^^^^^
       mov     ecx,edx
       shl     ecx,3
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                               ^^^^^^^
       cmp     ecx,eax
       jge     M01_L12
                       total += span1[i] * span2[i];
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       cmp     ecx,ebp
       jae     00007ffc`f8ed4bd6
       movsxd  rdx,ecx
       vmovss  xmm0,dword ptr [rbx+rdx*4]
       cmp     ecx,r14d
       jae     00007ffc`f8ed4bd6
       movsxd  rdx,ecx
       vmulss  xmm0,xmm0,dword ptr [rsi+rdx*4]
       vaddss  xmm0,xmm0,xmm1
       vmovaps xmm1,xmm0
                   for (i = vectCnt * vectLen; i < cnt; i++)
                                                        ^^^
       inc     ecx
       cmp     ecx,eax
       jl      M01_L11
M01_L12:
       cmp     ebp,r14d
       je      M01_L16
       cmp     ebp,r14d
       jg      M01_L13
                       for (var j = cnt; j < h.Length; j++)
                            ^^^^^^^^^^^
       jmp     M01_L14
M01_L13:
       mov     rsi,rbx
       mov     r14d,ebp
M01_L14:
       cmp     eax,r14d
       jge     M01_L16
                           total += h[j];
                           ^^^^^^^^^^^^^^
M01_L15:
       cmp     eax,r14d
       jae     00007ffc`f8ed4bd6
       movsxd  rdx,eax
       vaddss  xmm1,xmm1,dword ptr [rsi+rdx*4]
                       for (var j = cnt; j < h.Length; j++)
                                                       ^^^
       inc     eax
       cmp     eax,r14d
       jl      M01_L15
                   return total;
                   ^^^^^^^^^^^^^
M01_L16:
       vmovaps xmm0,xmm1
       vzeroupper
       add     rsp,60h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r14
       ret
M01_L17:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L18:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L19:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L20:
       mov     ecx,8
       call    System.Numerics.Vector.ThrowInsufficientNumberOfElementsException(Int32)
       int     3
M01_L21:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
; Total bytes of code 500
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyClassicSingle()
                   return DotMultiplyClassic(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyClassic(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyClassic(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rax,qword ptr [rdx]
       mov     esi,dword ptr [rdx+8]
       mov     edi,dword ptr [rdx+0Ch]
       mov     rbx,qword ptr [rcx]
       mov     ebp,dword ptr [rcx+8]
       mov     r14d,dword ptr [rcx+0Ch]
       jmp     M01_L00
                   var hs = hv.Span;
                   ^^^^^^^^^^^^^^^^^
       mov     rax,qword ptr [rcx]
       mov     esi,dword ptr [rcx+8]
       mov     edi,dword ptr [rcx+0Ch]
       mov     rbx,qword ptr [rdx]
       mov     ebp,dword ptr [rdx+8]
       mov     r14d,dword ptr [rdx+0Ch]
M01_L00:
       xor     r15d,r15d
       xor     r12d,r12d
       test    rax,rax
       je      M01_L03
       lea     rdx,[rax+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L01
       lea     r15,[rax+10h]
       mov     r12d,dword ptr [rax+8]
       jmp     M01_L02
M01_L01:
       lea     rdx,[rsp+30h]
       mov     rcx,rax
       mov     rax,qword ptr [rax]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     r15,qword ptr [rsp+30h]
       mov     r12d,dword ptr [rsp+38h]
M01_L02:
       and     esi,7FFFFFFFh
       mov     edx,esi
       mov     ecx,edi
       add     rcx,rdx
       mov     eax,r12d
       cmp     rcx,rax
       ja      M01_L11
       lea     r15,[r15+rdx*4]
       mov     r12d,edi
                   var ls = lv.Span;
                   ^^^^^^^^^^^^^^^^^
M01_L03:
       xor     esi,esi
       xor     edi,edi
       mov     rcx,rbx
       test    rcx,rcx
       je      M01_L06
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L04
       lea     rsi,[rcx+10h]
       mov     edi,dword ptr [rcx+8]
       jmp     M01_L05
M01_L04:
       lea     rdx,[rsp+20h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rsi,qword ptr [rsp+20h]
       mov     edi,dword ptr [rsp+28h]
M01_L05:
       and     ebp,7FFFFFFFh
       mov     eax,ebp
       mov     edx,r14d
       add     rdx,rax
       mov     ecx,edi
       cmp     rdx,rcx
       ja      M01_L12
       lea     rsi,[rsi+rax*4]
       mov     edi,r14d
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
M01_L06:
       vxorps  xmm0,xmm0,xmm0
                   var i = 0;
                   ^^^^^^^^^^
       xor     eax,eax
       test    edi,edi
       jle     M01_L08
                       total += hs[i] * ls[i];
                       ^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       cmp     eax,r12d
       jae     00007ffc`f8eef78a
       movsxd  rdx,eax
       vmovss  xmm1,dword ptr [r15+rdx*4]
       movsxd  rdx,eax
       vmulss  xmm1,xmm1,dword ptr [rsi+rdx*4]
       vaddss  xmm0,xmm0,xmm1
                   for (; i < ls.Length; i++)
                                         ^^^
       inc     eax
       cmp     eax,edi
       jl      M01_L07
M01_L08:
       cmp     eax,r12d
       jge     M01_L10
                       total += hs[i];
                       ^^^^^^^^^^^^^^^
M01_L09:
       cmp     eax,r12d
       jae     00007ffc`f8eef78a
       movsxd  rdx,eax
       vaddss  xmm0,xmm0,dword ptr [r15+rdx*4]
                   for (; i < hs.Length; i++)
                                         ^^^
       inc     eax
       cmp     eax,r12d
       jl      M01_L09
                   return total;
                   ^^^^^^^^^^^^^
M01_L10:
       add     rsp,40h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r12
       pop     r14
       pop     r15
       ret
M01_L11:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L12:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
; Total bytes of code 315
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyClassicSingleWPtr()
                   return DotMultiplyClassicWPtr(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyClassicWPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyClassicWPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rax,qword ptr [rdx]
       mov     esi,dword ptr [rdx+8]
       mov     edi,dword ptr [rdx+0Ch]
       mov     rbx,qword ptr [rcx]
       mov     ebp,dword ptr [rcx+8]
       mov     r14d,dword ptr [rcx+0Ch]
       jmp     M01_L00
                   var hl = hv.Length;
                   ^^^^^^^^^^^^^^^^^^^
       mov     rax,qword ptr [rcx]
       mov     esi,dword ptr [rcx+8]
       mov     edi,dword ptr [rcx+0Ch]
       mov     rbx,qword ptr [rdx]
       mov     ebp,dword ptr [rdx+8]
       mov     r14d,dword ptr [rdx+0Ch]
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
M01_L00:
       vxorps  xmm6,xmm6,xmm6
                         var hs = (float*) Unsafe.AsPointer(ref hv.Span[0]);
                         ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       xor     r15d,r15d
       xor     r12d,r12d
       test    rax,rax
       je      M01_L03
       lea     rdx,[rax+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L01
       lea     r15,[rax+10h]
       mov     r12d,dword ptr [rax+8]
       jmp     M01_L02
M01_L01:
       lea     rdx,[rsp+30h]
       mov     rcx,rax
       mov     rax,qword ptr [rax]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     r15,qword ptr [rsp+30h]
       mov     r12d,dword ptr [rsp+38h]
M01_L02:
       and     esi,7FFFFFFFh
       mov     edx,esi
       mov     ecx,edi
       add     rcx,rdx
       mov     eax,r12d
       cmp     rcx,rax
       ja      M01_L13
       lea     r15,[r15+rdx*4]
       mov     r12d,edi
M01_L03:
       cmp     r12d,0
       jbe     00007ffc`f8ec44f6
                       var ls = (float*) Unsafe.AsPointer(ref lv.Span[0]);
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       xor     esi,esi
       xor     r12d,r12d
       mov     rcx,rbx
       test    rcx,rcx
       je      M01_L06
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L04
       lea     rsi,[rcx+10h]
       mov     r12d,dword ptr [rcx+8]
       jmp     M01_L05
M01_L04:
       lea     rdx,[rsp+20h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rsi,qword ptr [rsp+20h]
       mov     r12d,dword ptr [rsp+28h]
M01_L05:
       and     ebp,7FFFFFFFh
       mov     eax,ebp
       mov     edx,r14d
       add     rdx,rax
       mov     ecx,r12d
       cmp     rdx,rcx
       ja      M01_L14
       lea     rsi,[rsi+rax*4]
       mov     r12d,r14d
M01_L06:
       cmp     r12d,0
       jbe     00007ffc`f8ec44f6
                       var i = 0;
                       ^^^^^^^^^^
       xor     eax,eax
                       for (; i < ll; i++)
                              ^^^^^^
       test    r14d,r14d
       jle     M01_L12
                           total += *hs++ * *ls++;
                           ^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       lea     rdx,[r15+4]
       mov     rbx,rdx
       lea     rdx,[rsi+4]
       vmovss  xmm0,dword ptr [r15]
       vmulss  xmm0,xmm0,dword ptr [rsi]
       vaddss  xmm6,xmm6,xmm0
                       for (; i < ll; i++)
                                      ^^^
       inc     eax
       cmp     eax,r14d
       mov     rsi,rdx
       jl      M01_L11
                       for (; i < hl; i++)
                              ^^^^^^
M01_L08:
       cmp     eax,edi
       jge     M01_L10
                           total += *hs++;
                           ^^^^^^^^^^^^^^^
M01_L09:
       mov     rdx,rbx
       lea     rbx,[rdx+4]
       vaddss  xmm6,xmm6,dword ptr [rdx]
                       for (; i < hl; i++)
                                      ^^^
       inc     eax
       cmp     eax,edi
       jl      M01_L09
                   return total;
                   ^^^^^^^^^^^^^
M01_L10:
       vmovaps xmm0,xmm6
       vmovaps xmm6,xmmword ptr [rsp+40h]
       add     rsp,50h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r12
       pop     r14
       pop     r15
       ret
M01_L11:
       mov     r15,rbx
       jmp     M01_L07
M01_L12:
       mov     rbx,r15
       jmp     M01_L08
M01_L13:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L14:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
; Total bytes of code 353
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyClassicGroup4()
                   return DotMultiplyClassicGroup4(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyClassicGroup4(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyClassicGroup4(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rax,qword ptr [rdx]
       mov     esi,dword ptr [rdx+8]
       mov     edi,dword ptr [rdx+0Ch]
       mov     rbx,qword ptr [rcx]
       mov     ebp,dword ptr [rcx+8]
       mov     r14d,dword ptr [rcx+0Ch]
       jmp     M01_L00
                   var hs = hv.Span;
                   ^^^^^^^^^^^^^^^^^
       mov     rax,qword ptr [rcx]
       mov     esi,dword ptr [rcx+8]
       mov     edi,dword ptr [rcx+0Ch]
       mov     rbx,qword ptr [rdx]
       mov     ebp,dword ptr [rdx+8]
       mov     r14d,dword ptr [rdx+0Ch]
M01_L00:
       xor     r15d,r15d
       xor     r12d,r12d
       test    rax,rax
       je      M01_L03
       lea     rdx,[rax+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L01
       lea     r15,[rax+10h]
       mov     r12d,dword ptr [rax+8]
       jmp     M01_L02
M01_L01:
       lea     rdx,[rsp+30h]
       mov     rcx,rax
       mov     rax,qword ptr [rax]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     r15,qword ptr [rsp+30h]
       mov     r12d,dword ptr [rsp+38h]
M01_L02:
       and     esi,7FFFFFFFh
       mov     edx,esi
       mov     ecx,edi
       add     rcx,rdx
       mov     eax,r12d
       cmp     rcx,rax
       ja      M01_L14
       lea     r15,[r15+rdx*4]
       mov     r12d,edi
                   var ls = lv.Span;
                   ^^^^^^^^^^^^^^^^^
M01_L03:
       xor     esi,esi
       xor     edi,edi
       mov     rcx,rbx
       test    rcx,rcx
       je      M01_L06
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L04
       lea     rsi,[rcx+10h]
       mov     edi,dword ptr [rcx+8]
       jmp     M01_L05
M01_L04:
       lea     rdx,[rsp+20h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rsi,qword ptr [rsp+20h]
       mov     edi,dword ptr [rsp+28h]
M01_L05:
       and     ebp,7FFFFFFFh
       mov     eax,ebp
       mov     edx,r14d
       add     rdx,rax
       mov     ecx,edi
       cmp     rdx,rcx
       ja      M01_L15
       lea     rsi,[rsi+rax*4]
       mov     edi,r14d
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
M01_L06:
       vxorps  xmm0,xmm0,xmm0
                   var i = 0;
                   ^^^^^^^^^^
       xor     eax,eax
       test    edi,edi
       jle     M01_L08
                       total += hs[i] * ls[i] + hs[i + 1] * ls[i + 1] + hs[i + 2] * ls[i + 2] + hs[i + 3] * ls[i + 3];
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       cmp     eax,r12d
       jae     00007ffc`f8ec4577
       movsxd  rdx,eax
       vmovss  xmm1,dword ptr [r15+rdx*4]
       cmp     eax,edi
       jae     00007ffc`f8ec4577
       movsxd  rdx,eax
       vmulss  xmm1,xmm1,dword ptr [rsi+rdx*4]
       lea     edx,[rax+1]
       cmp     edx,r12d
       jae     00007ffc`f8ec4577
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4577
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       lea     edx,[rax+2]
       cmp     edx,r12d
       jae     00007ffc`f8ec4577
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4577
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       lea     edx,[rax+3]
       cmp     edx,r12d
       jae     00007ffc`f8ec4577
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4577
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       vaddss  xmm0,xmm0,xmm1
                   for (; i < ls.Length; i += grp)
                                         ^^^^^^^^
       add     eax,4
       cmp     eax,edi
       jl      M01_L07
M01_L08:
       mov     edx,edi
       sar     edx,1Fh
       and     edx,3
       add     edx,edi
       and     edx,0FFFFFFFCh
       mov     ecx,edi
       sub     ecx,edx
       je      M01_L12
                       for (i += grp; i < ls.Length; i++)
                            ^^^^^^^^
       add     eax,4
       cmp     eax,edi
       jge     M01_L10
                           total += hs[i] * ls[i];
                           ^^^^^^^^^^^^^^^^^^^^^^^
M01_L09:
       cmp     eax,r12d
       jae     00007ffc`f8ec4577
       movsxd  rdx,eax
       vmovss  xmm1,dword ptr [r15+rdx*4]
       cmp     eax,edi
       jae     00007ffc`f8ec4577
       movsxd  rdx,eax
       vmulss  xmm1,xmm1,dword ptr [rsi+rdx*4]
       vaddss  xmm1,xmm1,xmm0
       vmovaps xmm0,xmm1
                       for (i += grp; i < ls.Length; i++)
                                                     ^^^
       inc     eax
       cmp     eax,edi
       jl      M01_L09
M01_L10:
       cmp     eax,r12d
       jge     M01_L13
                       total += hs[i];
                       ^^^^^^^^^^^^^^^
M01_L11:
       cmp     eax,r12d
       jae     00007ffc`f8ec4577
       movsxd  rdx,eax
       vaddss  xmm0,xmm0,dword ptr [r15+rdx*4]
                   for (; i < hs.Length; i++)
                                         ^^^
       inc     eax
M01_L12:
       cmp     eax,r12d
       jl      M01_L11
                   return total;
                   ^^^^^^^^^^^^^
M01_L13:
       add     rsp,40h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r12
       pop     r14
       pop     r15
       ret
M01_L14:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L15:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
; Total bytes of code 520
```

## .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyClassicGroup8()
                   return DotMultiplyClassicGroup8(ref m_Ma1, ref m_Ma2);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rcx,qword ptr [rbp+10h]
       cmp     dword ptr [rcx],ecx
       mov     rcx,qword ptr [rbp+10h]
       add     rcx,8
       mov     rdx,qword ptr [rbp+10h]
       cmp     dword ptr [rdx],edx
       mov     rdx,qword ptr [rbp+10h]
       add     rdx,18h
       call    CoreNN.Tools.Multipliers.DotMultiplyClassicGroup8(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 33
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyClassicGroup8(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
                   var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       mov     rax,qword ptr [rdx]
       mov     esi,dword ptr [rdx+8]
       mov     edi,dword ptr [rdx+0Ch]
       mov     rbx,qword ptr [rcx]
       mov     ebp,dword ptr [rcx+8]
       mov     r14d,dword ptr [rcx+0Ch]
       jmp     M01_L00
                   var hs = hv.Span;
                   ^^^^^^^^^^^^^^^^^
       mov     rax,qword ptr [rcx]
       mov     esi,dword ptr [rcx+8]
       mov     edi,dword ptr [rcx+0Ch]
       mov     rbx,qword ptr [rdx]
       mov     ebp,dword ptr [rdx+8]
       mov     r14d,dword ptr [rdx+0Ch]
M01_L00:
       xor     r15d,r15d
       xor     r12d,r12d
       test    rax,rax
       je      M01_L03
       lea     rdx,[rax+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L01
       lea     r15,[rax+10h]
       mov     r12d,dword ptr [rax+8]
       jmp     M01_L02
M01_L01:
       lea     rdx,[rsp+30h]
       mov     rcx,rax
       mov     rax,qword ptr [rax]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     r15,qword ptr [rsp+30h]
       mov     r12d,dword ptr [rsp+38h]
M01_L02:
       and     esi,7FFFFFFFh
       mov     edx,esi
       mov     ecx,edi
       add     rcx,rdx
       mov     eax,r12d
       cmp     rcx,rax
       ja      M01_L13
       lea     r15,[r15+rdx*4]
       mov     r12d,edi
                   var ls = lv.Span;
                   ^^^^^^^^^^^^^^^^^
M01_L03:
       xor     esi,esi
       xor     edi,edi
       mov     rcx,rbx
       test    rcx,rcx
       je      M01_L06
       lea     rdx,[rcx+8]
       mov     rdx,qword ptr [rdx-8]
       cmp     dword ptr [rdx],0
       jge     M01_L04
       lea     rsi,[rcx+10h]
       mov     edi,dword ptr [rcx+8]
       jmp     M01_L05
M01_L04:
       lea     rdx,[rsp+20h]
       mov     rax,qword ptr [rcx]
       mov     rax,qword ptr [rax+40h]
       call    qword ptr [rax+28h]
       mov     rsi,qword ptr [rsp+20h]
       mov     edi,dword ptr [rsp+28h]
M01_L05:
       and     ebp,7FFFFFFFh
       mov     eax,ebp
       mov     edx,r14d
       add     rdx,rax
       mov     ecx,edi
       cmp     rdx,rcx
       ja      M01_L14
       lea     rsi,[rsi+rax*4]
       mov     edi,r14d
                   var total = 0f;
                   ^^^^^^^^^^^^^^^
M01_L06:
       vxorps  xmm0,xmm0,xmm0
                   var i = 0;
                   ^^^^^^^^^^
       xor     eax,eax
       test    edi,edi
       jle     M01_L08
                       total += hs[i] * ls[i] + hs[i + 1] * ls[i + 1] + hs[i + 2] * ls[i + 2] + hs[i + 3] * ls[i + 3]
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                                + hs[i + 4] * ls[i + 4] + hs[i + 5] * ls[i + 5] + hs[i + 6] * ls[i + 6] + hs[i + 7] * ls[i + 7];
                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       cmp     eax,r12d
       jae     00007ffc`f8ec4637
       movsxd  rdx,eax
       vmovss  xmm1,dword ptr [r15+rdx*4]
       cmp     eax,edi
       jae     00007ffc`f8ec4637
       movsxd  rdx,eax
       vmulss  xmm1,xmm1,dword ptr [rsi+rdx*4]
       lea     edx,[rax+1]
       cmp     edx,r12d
       jae     00007ffc`f8ec4637
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4637
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       lea     edx,[rax+2]
       cmp     edx,r12d
       jae     00007ffc`f8ec4637
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4637
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       lea     edx,[rax+3]
       cmp     edx,r12d
       jae     00007ffc`f8ec4637
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4637
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       lea     edx,[rax+4]
       cmp     edx,r12d
       jae     00007ffc`f8ec4637
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4637
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       lea     edx,[rax+5]
       cmp     edx,r12d
       jae     00007ffc`f8ec4637
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4637
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       lea     edx,[rax+6]
       cmp     edx,r12d
       jae     00007ffc`f8ec4637
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4637
       vmulss  xmm2,xmm2,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm2
       lea     edx,[rax+7]
       cmp     edx,r12d
       jae     00007ffc`f8ec4637
       movsxd  rcx,edx
       vmovss  xmm2,dword ptr [r15+rcx*4]
       cmp     edx,edi
       jae     00007ffc`f8ec4637
       vmovss  xmm3,dword ptr [rsi+rcx*4]
       vmulss  xmm2,xmm2,xmm3
       vaddss  xmm1,xmm1,xmm2
       vaddss  xmm0,xmm0,xmm1
                   for (; i < ls.Length; i += grp)
                                         ^^^^^^^^
       add     eax,8
       cmp     eax,edi
       jl      M01_L07
M01_L08:
       mov     edx,edi
       sar     edx,1Fh
       and     edx,7
       add     edx,edi
       and     edx,0FFFFFFF8h
       mov     ecx,edi
       sub     ecx,edx
       je      M01_L10
       mov     edx,edi
                       for (i += grp; i < len; i++)
                            ^^^^^^^^
       add     eax,8
                       for (i += grp; i < len; i++)
                                      ^^^^^^^
       cmp     eax,edi
       jge     M01_L10
                           total += hs[i] * ls[i];
                           ^^^^^^^^^^^^^^^^^^^^^^^
M01_L09:
       cmp     eax,r12d
       jae     00007ffc`f8ec4637
       movsxd  rcx,eax
       vmovss  xmm1,dword ptr [r15+rcx*4]
       cmp     eax,edx
       jae     00007ffc`f8ec4637
       movsxd  rcx,eax
       vmulss  xmm1,xmm1,dword ptr [rsi+rcx*4]
       vaddss  xmm1,xmm1,xmm0
       vmovaps xmm0,xmm1
                       for (i += grp; i < len; i++)
                                               ^^^
       inc     eax
       cmp     eax,edi
       jl      M01_L09
M01_L10:
       mov     edx,r12d
                   for (; i < ln; i++)
                          ^^^^^^
       cmp     eax,r12d
       jge     M01_L12
                       total += hs[i];
                       ^^^^^^^^^^^^^^^
M01_L11:
       cmp     eax,edx
       jae     00007ffc`f8ec4637
       movsxd  rcx,eax
       vaddss  xmm0,xmm0,dword ptr [r15+rcx*4]
                   for (; i < ln; i++)
                                  ^^^
       inc     eax
       cmp     eax,r12d
       jl      M01_L11
                   return total;
                   ^^^^^^^^^^^^^
M01_L12:
       add     rsp,40h
       pop     rbx
       pop     rbp
       pop     rsi
       pop     rdi
       pop     r12
       pop     r14
       pop     r15
       ret
M01_L13:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
M01_L14:
       call    System.ThrowHelper.ThrowArgumentOutOfRangeException()
       int     3
; Total bytes of code 680
```

