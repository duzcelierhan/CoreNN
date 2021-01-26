## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyAvx()
;             return DotMultiplyIntrinsicWAvx(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142F6F0 cmp       [rcx],ecx
       7FF83142F6F2 mov       [rsp+8],rcx
       7FF83142F6F7 add       rcx,8
       7FF83142F6FB mov       rdx,[rsp+8]
       7FF83142F700 add       rdx,18
       7FF83142F704 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWAvx(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWAvx(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF831429F20 push      r15
       7FF831429F22 push      r14
       7FF831429F24 push      rdi
       7FF831429F25 push      rsi
       7FF831429F26 push      rbp
       7FF831429F27 push      rbx
       7FF831429F28 sub       rsp,68
       7FF831429F2C vzeroupper
       7FF831429F2F xor       eax,eax
       7FF831429F31 mov       [rsp+48],rax
       7FF831429F36 vxorps    xmm4,xmm4,xmm4
       7FF831429F3A vmovdqa   xmmword ptr [rsp+50],xmm4
       7FF831429F40 mov       [rsp+60],rax
       7FF831429F45 mov       rsi,rcx
       7FF831429F48 mov       rdi,rdx
;             var span1 = vector1.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831429F4B xor       ebx,ebx
       7FF831429F4D xor       ebp,ebp
       7FF831429F4F mov       rcx,[rsi]
       7FF831429F52 test      rcx,rcx
       7FF831429F55 je        short M01_L02
       7FF831429F57 mov       rdx,[rcx]
       7FF831429F5A test      dword ptr [rdx],80000000
       7FF831429F60 je        short M01_L00
       7FF831429F62 lea       rbx,[rcx+10]
       7FF831429F66 mov       ebp,[rcx+8]
       7FF831429F69 jmp       short M01_L01
M01_L00:
       7FF831429F6B lea       rdx,[rsp+58]
       7FF831429F70 mov       rax,[rcx]
       7FF831429F73 mov       rax,[rax+40]
       7FF831429F77 call      qword ptr [rax+28]
       7FF831429F7A mov       rbx,[rsp+58]
       7FF831429F7F mov       ebp,[rsp+60]
M01_L01:
       7FF831429F83 mov       edx,[rsi+8]
       7FF831429F86 and       edx,7FFFFFFF
       7FF831429F8C mov       ecx,[rsi+0C]
       7FF831429F8F mov       eax,ecx
       7FF831429F91 add       rax,rdx
       7FF831429F94 mov       r8d,ebp
       7FF831429F97 cmp       rax,r8
       7FF831429F9A ja        near ptr M01_L17
       7FF831429FA0 lea       rbx,[rbx+rdx*4]
       7FF831429FA4 mov       ebp,ecx
;             var span2 = vector2.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       7FF831429FA6 xor       r14d,r14d
       7FF831429FA9 xor       r15d,r15d
       7FF831429FAC mov       rcx,[rdi]
       7FF831429FAF test      rcx,rcx
       7FF831429FB2 je        short M01_L05
       7FF831429FB4 mov       rdx,[rcx]
       7FF831429FB7 test      dword ptr [rdx],80000000
       7FF831429FBD je        short M01_L03
       7FF831429FBF lea       r14,[rcx+10]
       7FF831429FC3 mov       r15d,[rcx+8]
       7FF831429FC7 jmp       short M01_L04
M01_L03:
       7FF831429FC9 lea       rdx,[rsp+48]
       7FF831429FCE mov       rax,[rcx]
       7FF831429FD1 mov       rax,[rax+40]
       7FF831429FD5 call      qword ptr [rax+28]
       7FF831429FD8 mov       r14,[rsp+48]
       7FF831429FDD mov       r15d,[rsp+50]
M01_L04:
       7FF831429FE2 mov       eax,[rdi+8]
       7FF831429FE5 and       eax,7FFFFFFF
       7FF831429FEA mov       edx,[rdi+0C]
       7FF831429FED mov       ecx,edx
       7FF831429FEF add       rcx,rax
       7FF831429FF2 mov       r8d,r15d
       7FF831429FF5 cmp       rcx,r8
       7FF831429FF8 ja        near ptr M01_L17
       7FF831429FFE lea       r14,[r14+rax*4]
       7FF83142A002 mov       r15d,edx
M01_L05:
       7FF83142A005 mov       eax,ebp
       7FF83142A007 mov       edx,r15d
       7FF83142A00A cmp       eax,edx
       7FF83142A00C jle       short M01_L06
       7FF83142A00E mov       eax,r15d
       7FF83142A011 jmp       short M01_L07
M01_L06:
       7FF83142A013 mov       eax,ebp
;             var v3 = Vector256.CreateScalarUnsafe(0f);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       7FF83142A015 vxorps    xmm0,xmm0,xmm0
;             var vectCnt = cnt / vectLen;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142A019 mov       edx,eax
       7FF83142A01B sar       edx,1F
       7FF83142A01E and       edx,7
       7FF83142A021 add       edx,eax
       7FF83142A023 sar       edx,3
;                 for (i = 0; i < vectCnt; i++)
;                      ^^^^^
       7FF83142A026 xor       ecx,ecx
       7FF83142A028 test      edx,edx
       7FF83142A02A jle       short M01_L09
;                     var index = i * vectLen;
;                     ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF83142A02C mov       r8d,ecx
       7FF83142A02F shl       r8d,3
;                     var v1 = Avx.LoadVector256((float*)Unsafe.AsPointer(ref span1[index]));
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142A033 cmp       r8d,ebp
       7FF83142A036 jae       near ptr M01_L19
       7FF83142A03C movsxd    r9,r8d
       7FF83142A03F shl       r9,2
       7FF83142A043 lea       r10,[rbx+r9]
       7FF83142A047 vmovups   ymm1,[r10]
;                     var v2 = Avx.LoadVector256((float*)Unsafe.AsPointer(ref span2[index]));
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142A04C cmp       r8d,r15d
       7FF83142A04F jae       near ptr M01_L19
       7FF83142A055 add       r9,r14
       7FF83142A058 vmovups   ymm2,[r9]
;                     var t = Avx.Multiply(v1, v2);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142A05D vmulps    ymm1,ymm1,ymm2
;                     v3 = Avx.Add(v3, t);
;                     ^^^^^^^^^^^^^^^^^^^^
       7FF83142A061 vaddps    ymm0,ymm0,ymm1
       7FF83142A065 inc       ecx
       7FF83142A067 cmp       ecx,edx
       7FF83142A069 jl        short M01_L08
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
M01_L09:
       7FF83142A06B vxorps    xmm1,xmm1,xmm1
;             for (i = 0; i < vectLen; i++)
;                  ^^^^^
       7FF83142A06F xor       ecx,ecx
;                 total += v3.GetElement(i);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       7FF83142A071 vmovupd   [rsp+20],ymm0
       7FF83142A077 cmp       ecx,8
       7FF83142A07A jae       near ptr M01_L18
       7FF83142A080 lea       r8,[rsp+20]
       7FF83142A085 movsxd    r9,ecx
       7FF83142A088 vmovss    xmm2,dword ptr [r8+r9*4]
       7FF83142A08E vaddss    xmm1,xmm1,xmm2
       7FF83142A092 inc       ecx
       7FF83142A094 cmp       ecx,8
       7FF83142A097 jl        short M01_L10
;             for (i = vectCnt * vectLen; i < cnt; i++)
;                  ^^^^^^^^^^^^^^^^^^^^^
       7FF83142A099 mov       ecx,edx
       7FF83142A09B shl       ecx,3
       7FF83142A09E cmp       ecx,eax
       7FF83142A0A0 jge       short M01_L12
;                 total += span1[i] * span2[i];
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       7FF83142A0A2 cmp       ecx,ebp
       7FF83142A0A4 jae       short M01_L19
       7FF83142A0A6 movsxd    rdx,ecx
       7FF83142A0A9 vmovss    xmm0,dword ptr [rbx+rdx*4]
       7FF83142A0AE cmp       ecx,r15d
       7FF83142A0B1 jae       short M01_L19
       7FF83142A0B3 vmulss    xmm0,xmm0,dword ptr [r14+rdx*4]
       7FF83142A0B9 vaddss    xmm1,xmm0,xmm1
       7FF83142A0BD inc       ecx
       7FF83142A0BF cmp       ecx,eax
       7FF83142A0C1 jl        short M01_L11
M01_L12:
       7FF83142A0C3 mov       edx,[rsi+0C]
       7FF83142A0C6 cmp       edx,[rdi+0C]
       7FF83142A0C9 je        short M01_L16
       7FF83142A0CB mov       edx,[rsi+0C]
       7FF83142A0CE cmp       edx,[rdi+0C]
       7FF83142A0D1 jg        short M01_L13
;                   for (var j = cnt; j < h.Length; j++)
;                        ^^^^^^^^^^^
       7FF83142A0D3 jmp       short M01_L14
M01_L13:
       7FF83142A0D5 mov       r14,rbx
       7FF83142A0D8 mov       r15d,ebp
M01_L14:
       7FF83142A0DB cmp       eax,r15d
       7FF83142A0DE jge       short M01_L16
;                     total += h[j];
;                     ^^^^^^^^^^^^^^
M01_L15:
       7FF83142A0E0 cmp       eax,r15d
       7FF83142A0E3 jae       short M01_L19
       7FF83142A0E5 movsxd    rdx,eax
       7FF83142A0E8 vaddss    xmm1,xmm1,dword ptr [r14+rdx*4]
       7FF83142A0EE inc       eax
       7FF83142A0F0 cmp       eax,r15d
       7FF83142A0F3 jl        short M01_L15
;             return total;
;             ^^^^^^^^^^^^^
M01_L16:
       7FF83142A0F5 vmovaps   xmm0,xmm1
       7FF83142A0F9 vzeroupper
       7FF83142A0FC add       rsp,68
       7FF83142A100 pop       rbx
       7FF83142A101 pop       rbp
       7FF83142A102 pop       rsi
       7FF83142A103 pop       rdi
       7FF83142A104 pop       r14
       7FF83142A106 pop       r15
       7FF83142A108 ret
M01_L17:
       7FF83142A109 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF83142A10E int       3
M01_L18:
       7FF83142A10F mov       ecx,15
       7FF83142A114 call      System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       7FF83142A119 int       3
M01_L19:
       7FF83142A11A call      CORINFO_HELP_RNGCHKFAIL
       7FF83142A11F int       3
; Total bytes of code 512
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyAvxWSpanPtr()
;             return DotMultiplyIntrinsicWAvxWSpanPtr(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144FAF0 cmp       [rcx],ecx
       7FF83144FAF2 mov       [rsp+8],rcx
       7FF83144FAF7 add       rcx,8
       7FF83144FAFB mov       rdx,[rsp+8]
       7FF83144FB00 add       rdx,18
       7FF83144FB04 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWAvxWSpanPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWAvxWSpanPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF83144A1E0 push      r15
       7FF83144A1E2 push      r14
       7FF83144A1E4 push      rdi
       7FF83144A1E5 push      rsi
       7FF83144A1E6 push      rbp
       7FF83144A1E7 push      rbx
       7FF83144A1E8 sub       rsp,68
       7FF83144A1EC vzeroupper
       7FF83144A1EF xor       eax,eax
       7FF83144A1F1 mov       [rsp+48],rax
       7FF83144A1F6 vxorps    xmm4,xmm4,xmm4
       7FF83144A1FA vmovdqa   xmmword ptr [rsp+50],xmm4
       7FF83144A200 mov       [rsp+60],rax
       7FF83144A205 mov       rsi,rcx
       7FF83144A208 mov       rdi,rdx
;             var span1 = vector1.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A20B xor       ebx,ebx
       7FF83144A20D xor       ebp,ebp
       7FF83144A20F mov       rcx,[rsi]
       7FF83144A212 test      rcx,rcx
       7FF83144A215 je        short M01_L02
       7FF83144A217 mov       rdx,[rcx]
       7FF83144A21A test      dword ptr [rdx],80000000
       7FF83144A220 je        short M01_L00
       7FF83144A222 lea       rbx,[rcx+10]
       7FF83144A226 mov       ebp,[rcx+8]
       7FF83144A229 jmp       short M01_L01
M01_L00:
       7FF83144A22B lea       rdx,[rsp+58]
       7FF83144A230 mov       rax,[rcx]
       7FF83144A233 mov       rax,[rax+40]
       7FF83144A237 call      qword ptr [rax+28]
       7FF83144A23A mov       rbx,[rsp+58]
       7FF83144A23F mov       ebp,[rsp+60]
M01_L01:
       7FF83144A243 mov       edx,[rsi+8]
       7FF83144A246 and       edx,7FFFFFFF
       7FF83144A24C mov       ecx,[rsi+0C]
       7FF83144A24F mov       eax,ecx
       7FF83144A251 add       rax,rdx
       7FF83144A254 mov       r8d,ebp
       7FF83144A257 cmp       rax,r8
       7FF83144A25A ja        near ptr M01_L17
       7FF83144A260 lea       rbx,[rbx+rdx*4]
       7FF83144A264 mov       ebp,ecx
;             var span2 = vector2.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       7FF83144A266 xor       r14d,r14d
       7FF83144A269 xor       r15d,r15d
       7FF83144A26C mov       rcx,[rdi]
       7FF83144A26F test      rcx,rcx
       7FF83144A272 je        short M01_L05
       7FF83144A274 mov       rdx,[rcx]
       7FF83144A277 test      dword ptr [rdx],80000000
       7FF83144A27D je        short M01_L03
       7FF83144A27F lea       r14,[rcx+10]
       7FF83144A283 mov       r15d,[rcx+8]
       7FF83144A287 jmp       short M01_L04
M01_L03:
       7FF83144A289 lea       rdx,[rsp+48]
       7FF83144A28E mov       rax,[rcx]
       7FF83144A291 mov       rax,[rax+40]
       7FF83144A295 call      qword ptr [rax+28]
       7FF83144A298 mov       r14,[rsp+48]
       7FF83144A29D mov       r15d,[rsp+50]
M01_L04:
       7FF83144A2A2 mov       eax,[rdi+8]
       7FF83144A2A5 and       eax,7FFFFFFF
       7FF83144A2AA mov       edx,[rdi+0C]
       7FF83144A2AD mov       ecx,edx
       7FF83144A2AF add       rcx,rax
       7FF83144A2B2 mov       r8d,r15d
       7FF83144A2B5 cmp       rcx,r8
       7FF83144A2B8 ja        near ptr M01_L17
       7FF83144A2BE lea       r14,[r14+rax*4]
       7FF83144A2C2 mov       r15d,edx
M01_L05:
       7FF83144A2C5 mov       eax,ebp
       7FF83144A2C7 cmp       eax,r15d
       7FF83144A2CA jle       short M01_L06
       7FF83144A2CC mov       eax,r15d
       7FF83144A2CF jmp       short M01_L07
M01_L06:
       7FF83144A2D1 mov       eax,ebp
;             var v3 = Vector256.CreateScalarUnsafe(0f);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       7FF83144A2D3 vxorps    xmm0,xmm0,xmm0
;             var vectCnt = cnt / vectLen;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A2D7 mov       edx,eax
       7FF83144A2D9 sar       edx,1F
       7FF83144A2DC and       edx,7
       7FF83144A2DF add       edx,eax
       7FF83144A2E1 sar       edx,3
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
       7FF83144A2E4 vxorps    xmm1,xmm1,xmm1
;                 var ptr1 = (float*)Unsafe.AsPointer(ref span1[0]);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A2E8 cmp       ebp,0
       7FF83144A2EB jbe       near ptr M01_L19
       7FF83144A2F1 mov       rcx,rbx
;                 var ptr2 = (float*)Unsafe.AsPointer(ref span2[0]);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A2F4 cmp       r15d,0
       7FF83144A2F8 jbe       near ptr M01_L19
       7FF83144A2FE mov       r8,r14
;                 for (i = 0; i < vectCnt; i++)
;                      ^^^^^
       7FF83144A301 xor       r9d,r9d
       7FF83144A304 test      edx,edx
       7FF83144A306 jle       short M01_L09
;                     var v1 = Avx.LoadVector256(ptr1);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF83144A308 vmovups   ymm2,[rcx]
;                     var v2 = Avx.LoadVector256(ptr2);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A30C vmovups   ymm3,[r8]
;                     var t = Avx.Multiply(v1, v2);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A311 vmulps    ymm2,ymm2,ymm3
;                     v3 = Avx.Add(v3, t);
;                     ^^^^^^^^^^^^^^^^^^^^
       7FF83144A315 vaddps    ymm0,ymm0,ymm2
;                     ptr1 += vectLen;
;                     ^^^^^^^^^^^^^^^^
       7FF83144A319 add       rcx,20
;                     ptr2 += vectLen;
;                     ^^^^^^^^^^^^^^^^
       7FF83144A31D add       r8,20
       7FF83144A321 inc       r9d
       7FF83144A324 cmp       r9d,edx
       7FF83144A327 jl        short M01_L08
;                 for (i = 0; i < vectLen; i++)
;                      ^^^^^
M01_L09:
       7FF83144A329 xor       r9d,r9d
;                     total += v3.GetElement(i);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       7FF83144A32C vmovupd   [rsp+20],ymm0
       7FF83144A332 cmp       r9d,8
       7FF83144A336 jae       near ptr M01_L18
       7FF83144A33C lea       rcx,[rsp+20]
       7FF83144A341 movsxd    r8,r9d
       7FF83144A344 vmovss    xmm2,dword ptr [rcx+r8*4]
       7FF83144A34A vaddss    xmm1,xmm1,xmm2
       7FF83144A34E inc       r9d
       7FF83144A351 cmp       r9d,8
       7FF83144A355 jl        short M01_L10
;                 i = vectCnt * vectLen;
;                 ^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A357 mov       r9d,edx
       7FF83144A35A shl       r9d,3
;                 if (cnt % vectLen > 0)
;                 ^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A35E mov       edx,eax
       7FF83144A360 sar       edx,1F
       7FF83144A363 and       edx,7
       7FF83144A366 add       edx,eax
       7FF83144A368 and       edx,0FFFFFFF8
       7FF83144A36B mov       ecx,eax
       7FF83144A36D sub       ecx,edx
       7FF83144A36F test      ecx,ecx
       7FF83144A371 jle       short M01_L12
;                     ptr1 = (float*)Unsafe.AsPointer(ref span1[i]);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A373 cmp       r9d,ebp
       7FF83144A376 jae       near ptr M01_L19
       7FF83144A37C movsxd    r8,r9d
       7FF83144A37F shl       r8,2
       7FF83144A383 lea       rcx,[rbx+r8]
;                     ptr2 = (float*)Unsafe.AsPointer(ref span2[i]);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83144A387 cmp       r9d,r15d
       7FF83144A38A jae       near ptr M01_L19
       7FF83144A390 add       r8,r14
;                     for (; i < cnt; i++)
;                            ^^^^^^^
       7FF83144A393 cmp       r9d,eax
       7FF83144A396 jge       short M01_L12
;                         total += *ptr1++ * *ptr2++;
;                         ^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       7FF83144A398 mov       rdx,rcx
       7FF83144A39B lea       rcx,[rdx+4]
       7FF83144A39F mov       r10,r8
       7FF83144A3A2 lea       r8,[r10+4]
       7FF83144A3A6 vmovss    xmm0,dword ptr [rdx]
       7FF83144A3AA vmulss    xmm0,xmm0,dword ptr [r10]
       7FF83144A3AF vaddss    xmm1,xmm0,xmm1
       7FF83144A3B3 inc       r9d
       7FF83144A3B6 cmp       r9d,eax
       7FF83144A3B9 jl        short M01_L11
M01_L12:
       7FF83144A3BB mov       edx,[rsi+0C]
       7FF83144A3BE cmp       edx,[rdi+0C]
       7FF83144A3C1 je        short M01_L16
       7FF83144A3C3 mov       edx,[rsi+0C]
       7FF83144A3C6 cmp       edx,[rdi+0C]
       7FF83144A3C9 jg        short M01_L13
;                   for (var j = cnt; j < h.Length; j++)
;                        ^^^^^^^^^^^
       7FF83144A3CB jmp       short M01_L14
M01_L13:
       7FF83144A3CD mov       r14,rbx
       7FF83144A3D0 mov       r15d,ebp
M01_L14:
       7FF83144A3D3 cmp       eax,r15d
       7FF83144A3D6 jge       short M01_L16
;                     total += h[j];
;                     ^^^^^^^^^^^^^^
M01_L15:
       7FF83144A3D8 cmp       eax,r15d
       7FF83144A3DB jae       short M01_L19
       7FF83144A3DD movsxd    rdx,eax
       7FF83144A3E0 vaddss    xmm1,xmm1,dword ptr [r14+rdx*4]
       7FF83144A3E6 inc       eax
       7FF83144A3E8 cmp       eax,r15d
       7FF83144A3EB jl        short M01_L15
;             return total;
;             ^^^^^^^^^^^^^
M01_L16:
       7FF83144A3ED vmovaps   xmm0,xmm1
       7FF83144A3F1 vzeroupper
       7FF83144A3F4 add       rsp,68
       7FF83144A3F8 pop       rbx
       7FF83144A3F9 pop       rbp
       7FF83144A3FA pop       rsi
       7FF83144A3FB pop       rdi
       7FF83144A3FC pop       r14
       7FF83144A3FE pop       r15
       7FF83144A400 ret
M01_L17:
       7FF83144A401 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF83144A406 int       3
M01_L18:
       7FF83144A407 mov       ecx,15
       7FF83144A40C call      System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       7FF83144A411 int       3
M01_L19:
       7FF83144A412 call      CORINFO_HELP_RNGCHKFAIL
       7FF83144A417 int       3
; Total bytes of code 568
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyFma()
;             return DotMultiplyIntrinsicWFma(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83143F630 cmp       [rcx],ecx
       7FF83143F632 mov       [rsp+8],rcx
       7FF83143F637 add       rcx,8
       7FF83143F63B mov       rdx,[rsp+8]
       7FF83143F640 add       rdx,18
       7FF83143F644 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWFma(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWFma(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF831439D90 push      r14
       7FF831439D92 push      rdi
       7FF831439D93 push      rsi
       7FF831439D94 push      rbp
       7FF831439D95 push      rbx
       7FF831439D96 sub       rsp,70
       7FF831439D9A vzeroupper
       7FF831439D9D vxorps    xmm4,xmm4,xmm4
       7FF831439DA1 vmovdqa   xmmword ptr [rsp+50],xmm4
       7FF831439DA7 vmovdqa   xmmword ptr [rsp+60],xmm4
       7FF831439DAD mov       rsi,rcx
       7FF831439DB0 mov       rdi,rdx
;             var span1 = mem1.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^
       7FF831439DB3 xor       ebx,ebx
       7FF831439DB5 xor       ebp,ebp
       7FF831439DB7 mov       rcx,[rsi]
       7FF831439DBA test      rcx,rcx
       7FF831439DBD je        short M01_L02
       7FF831439DBF mov       rdx,[rcx]
       7FF831439DC2 test      dword ptr [rdx],80000000
       7FF831439DC8 je        short M01_L00
       7FF831439DCA lea       rbx,[rcx+10]
       7FF831439DCE mov       ebp,[rcx+8]
       7FF831439DD1 jmp       short M01_L01
M01_L00:
       7FF831439DD3 lea       rdx,[rsp+60]
       7FF831439DD8 mov       rax,[rcx]
       7FF831439DDB mov       rax,[rax+40]
       7FF831439DDF call      qword ptr [rax+28]
       7FF831439DE2 mov       rbx,[rsp+60]
       7FF831439DE7 mov       ebp,[rsp+68]
M01_L01:
       7FF831439DEB mov       edx,[rsi+8]
       7FF831439DEE and       edx,7FFFFFFF
       7FF831439DF4 mov       ecx,[rsi+0C]
       7FF831439DF7 mov       eax,ecx
       7FF831439DF9 add       rax,rdx
       7FF831439DFC mov       r8d,ebp
       7FF831439DFF cmp       rax,r8
       7FF831439E02 ja        near ptr M01_L17
       7FF831439E08 lea       rbx,[rbx+rdx*4]
       7FF831439E0C mov       ebp,ecx
;             var span2 = mem2.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       7FF831439E0E xor       esi,esi
       7FF831439E10 xor       r14d,r14d
       7FF831439E13 mov       rcx,[rdi]
       7FF831439E16 test      rcx,rcx
       7FF831439E19 je        short M01_L05
       7FF831439E1B mov       rdx,[rcx]
       7FF831439E1E test      dword ptr [rdx],80000000
       7FF831439E24 je        short M01_L03
       7FF831439E26 lea       rsi,[rcx+10]
       7FF831439E2A mov       r14d,[rcx+8]
       7FF831439E2E jmp       short M01_L04
M01_L03:
       7FF831439E30 lea       rdx,[rsp+50]
       7FF831439E35 mov       rax,[rcx]
       7FF831439E38 mov       rax,[rax+40]
       7FF831439E3C call      qword ptr [rax+28]
       7FF831439E3F mov       rsi,[rsp+50]
       7FF831439E44 mov       r14d,[rsp+58]
M01_L04:
       7FF831439E49 mov       eax,[rdi+8]
       7FF831439E4C and       eax,7FFFFFFF
       7FF831439E51 mov       edx,[rdi+0C]
       7FF831439E54 mov       ecx,edx
       7FF831439E56 add       rcx,rax
       7FF831439E59 mov       r8d,r14d
       7FF831439E5C cmp       rcx,r8
       7FF831439E5F ja        near ptr M01_L17
       7FF831439E65 lea       rsi,[rsi+rax*4]
       7FF831439E69 mov       r14d,edx
M01_L05:
       7FF831439E6C mov       eax,ebp
       7FF831439E6E mov       edx,r14d
       7FF831439E71 cmp       eax,edx
       7FF831439E73 jle       short M01_L06
       7FF831439E75 mov       eax,r14d
       7FF831439E78 jmp       short M01_L07
M01_L06:
       7FF831439E7A mov       eax,ebp
;             var v3 = Vector256.CreateScalarUnsafe(0f);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       7FF831439E7C vxorps    xmm0,xmm0,xmm0
;             var vectCnt = cnt / vectLen;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831439E80 mov       edx,eax
       7FF831439E82 sar       edx,1F
       7FF831439E85 and       edx,7
       7FF831439E88 add       edx,eax
       7FF831439E8A sar       edx,3
;                 for (i = 0; i < vectCnt; i++)
;                      ^^^^^
       7FF831439E8D xor       ecx,ecx
       7FF831439E8F test      edx,edx
       7FF831439E91 jle       short M01_L09
;                     var index = i * vectLen;
;                     ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF831439E93 mov       r8d,ecx
       7FF831439E96 shl       r8d,3
;                     var v1 = Avx.LoadVector256((float*) Unsafe.AsPointer(ref span1[index]));
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831439E9A cmp       r8d,ebp
       7FF831439E9D jae       near ptr M01_L19
       7FF831439EA3 movsxd    r9,r8d
       7FF831439EA6 shl       r9,2
       7FF831439EAA lea       r10,[rbx+r9]
       7FF831439EAE vmovups   ymm1,[r10]
;                     var v2 = Avx.LoadVector256((float*) Unsafe.AsPointer(ref span2[index]));
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831439EB3 cmp       r8d,r14d
       7FF831439EB6 jae       near ptr M01_L19
       7FF831439EBC add       r9,rsi
       7FF831439EBF vmovups   ymm2,[r9]
;                     v3 = Fma.MultiplyAdd(v1, v2, v3);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831439EC4 vfmadd213ps ymm1,ymm2,ymm0
       7FF831439EC9 vmovaps   ymm0,ymm1
       7FF831439ECD inc       ecx
       7FF831439ECF cmp       ecx,edx
       7FF831439ED1 jl        short M01_L08
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
M01_L09:
       7FF831439ED3 vxorps    xmm1,xmm1,xmm1
;             for (i = 0; i < vectLen; i++)
;                  ^^^^^
       7FF831439ED7 xor       ecx,ecx
;                 total += v3.GetElement(i);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       7FF831439ED9 vmovupd   [rsp+20],ymm0
       7FF831439EDF cmp       ecx,8
       7FF831439EE2 jae       near ptr M01_L18
       7FF831439EE8 lea       r8,[rsp+20]
       7FF831439EED movsxd    r9,ecx
       7FF831439EF0 vmovss    xmm2,dword ptr [r8+r9*4]
       7FF831439EF6 vaddss    xmm1,xmm1,xmm2
       7FF831439EFA inc       ecx
       7FF831439EFC cmp       ecx,8
       7FF831439EFF jl        short M01_L10
;             for (i = vectCnt * vectLen; i < cnt; i++)
;                  ^^^^^^^^^^^^^^^^^^^^^
       7FF831439F01 mov       ecx,edx
       7FF831439F03 shl       ecx,3
       7FF831439F06 cmp       ecx,eax
       7FF831439F08 jge       short M01_L12
;                 total += span1[i] * span2[i];
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       7FF831439F0A cmp       ecx,ebp
       7FF831439F0C jae       short M01_L19
       7FF831439F0E movsxd    rdx,ecx
       7FF831439F11 vmovss    xmm0,dword ptr [rbx+rdx*4]
       7FF831439F16 cmp       ecx,r14d
       7FF831439F19 jae       short M01_L19
       7FF831439F1B vmulss    xmm0,xmm0,dword ptr [rsi+rdx*4]
       7FF831439F20 vaddss    xmm1,xmm0,xmm1
       7FF831439F24 inc       ecx
       7FF831439F26 cmp       ecx,eax
       7FF831439F28 jl        short M01_L11
M01_L12:
       7FF831439F2A cmp       ebp,r14d
       7FF831439F2D je        short M01_L16
       7FF831439F2F cmp       ebp,r14d
       7FF831439F32 jg        short M01_L13
;                   for (var j = cnt; j < h.Length; j++)
;                        ^^^^^^^^^^^
       7FF831439F34 jmp       short M01_L14
M01_L13:
       7FF831439F36 mov       rsi,rbx
       7FF831439F39 mov       r14d,ebp
M01_L14:
       7FF831439F3C cmp       eax,r14d
       7FF831439F3F jge       short M01_L16
;                       total += h[j];
;                       ^^^^^^^^^^^^^^
M01_L15:
       7FF831439F41 cmp       eax,r14d
       7FF831439F44 jae       short M01_L19
       7FF831439F46 movsxd    rdx,eax
       7FF831439F49 vaddss    xmm1,xmm1,dword ptr [rsi+rdx*4]
       7FF831439F4E inc       eax
       7FF831439F50 cmp       eax,r14d
       7FF831439F53 jl        short M01_L15
;             return total;
;             ^^^^^^^^^^^^^
M01_L16:
       7FF831439F55 vmovaps   xmm0,xmm1
       7FF831439F59 vzeroupper
       7FF831439F5C add       rsp,70
       7FF831439F60 pop       rbx
       7FF831439F61 pop       rbp
       7FF831439F62 pop       rsi
       7FF831439F63 pop       rdi
       7FF831439F64 pop       r14
       7FF831439F66 ret
M01_L17:
       7FF831439F67 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF831439F6C int       3
M01_L18:
       7FF831439F6D mov       ecx,15
       7FF831439F72 call      System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       7FF831439F77 int       3
M01_L19:
       7FF831439F78 call      CORINFO_HELP_RNGCHKFAIL
       7FF831439F7D int       3
; Total bytes of code 494
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyFmaWSpanPtr()
;             return DotMultiplyIntrinsicWFmaWSpanPtr(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141FBF0 cmp       [rcx],ecx
       7FF83141FBF2 mov       [rsp+8],rcx
       7FF83141FBF7 add       rcx,8
       7FF83141FBFB mov       rdx,[rsp+8]
       7FF83141FC00 add       rdx,18
       7FF83141FC04 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWFmaWSpanPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWFmaWSpanPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF83141A2F0 push      r15
       7FF83141A2F2 push      r14
       7FF83141A2F4 push      rdi
       7FF83141A2F5 push      rsi
       7FF83141A2F6 push      rbp
       7FF83141A2F7 push      rbx
       7FF83141A2F8 sub       rsp,68
       7FF83141A2FC vzeroupper
       7FF83141A2FF xor       eax,eax
       7FF83141A301 mov       [rsp+48],rax
       7FF83141A306 vxorps    xmm4,xmm4,xmm4
       7FF83141A30A vmovdqa   xmmword ptr [rsp+50],xmm4
       7FF83141A310 mov       [rsp+60],rax
       7FF83141A315 mov       rsi,rcx
       7FF83141A318 mov       rdi,rdx
;             var span1 = vector1.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A31B xor       ebx,ebx
       7FF83141A31D xor       ebp,ebp
       7FF83141A31F mov       rcx,[rsi]
       7FF83141A322 test      rcx,rcx
       7FF83141A325 je        short M01_L02
       7FF83141A327 mov       rdx,[rcx]
       7FF83141A32A test      dword ptr [rdx],80000000
       7FF83141A330 je        short M01_L00
       7FF83141A332 lea       rbx,[rcx+10]
       7FF83141A336 mov       ebp,[rcx+8]
       7FF83141A339 jmp       short M01_L01
M01_L00:
       7FF83141A33B lea       rdx,[rsp+58]
       7FF83141A340 mov       rax,[rcx]
       7FF83141A343 mov       rax,[rax+40]
       7FF83141A347 call      qword ptr [rax+28]
       7FF83141A34A mov       rbx,[rsp+58]
       7FF83141A34F mov       ebp,[rsp+60]
M01_L01:
       7FF83141A353 mov       edx,[rsi+8]
       7FF83141A356 and       edx,7FFFFFFF
       7FF83141A35C mov       ecx,[rsi+0C]
       7FF83141A35F mov       eax,ecx
       7FF83141A361 add       rax,rdx
       7FF83141A364 mov       r8d,ebp
       7FF83141A367 cmp       rax,r8
       7FF83141A36A ja        near ptr M01_L17
       7FF83141A370 lea       rbx,[rbx+rdx*4]
       7FF83141A374 mov       ebp,ecx
;             var span2 = vector2.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       7FF83141A376 xor       r14d,r14d
       7FF83141A379 xor       r15d,r15d
       7FF83141A37C mov       rcx,[rdi]
       7FF83141A37F test      rcx,rcx
       7FF83141A382 je        short M01_L05
       7FF83141A384 mov       rdx,[rcx]
       7FF83141A387 test      dword ptr [rdx],80000000
       7FF83141A38D je        short M01_L03
       7FF83141A38F lea       r14,[rcx+10]
       7FF83141A393 mov       r15d,[rcx+8]
       7FF83141A397 jmp       short M01_L04
M01_L03:
       7FF83141A399 lea       rdx,[rsp+48]
       7FF83141A39E mov       rax,[rcx]
       7FF83141A3A1 mov       rax,[rax+40]
       7FF83141A3A5 call      qword ptr [rax+28]
       7FF83141A3A8 mov       r14,[rsp+48]
       7FF83141A3AD mov       r15d,[rsp+50]
M01_L04:
       7FF83141A3B2 mov       eax,[rdi+8]
       7FF83141A3B5 and       eax,7FFFFFFF
       7FF83141A3BA mov       edx,[rdi+0C]
       7FF83141A3BD mov       ecx,edx
       7FF83141A3BF add       rcx,rax
       7FF83141A3C2 mov       r8d,r15d
       7FF83141A3C5 cmp       rcx,r8
       7FF83141A3C8 ja        near ptr M01_L17
       7FF83141A3CE lea       r14,[r14+rax*4]
       7FF83141A3D2 mov       r15d,edx
M01_L05:
       7FF83141A3D5 mov       eax,ebp
       7FF83141A3D7 cmp       eax,r15d
       7FF83141A3DA jle       short M01_L06
       7FF83141A3DC mov       eax,r15d
       7FF83141A3DF jmp       short M01_L07
M01_L06:
       7FF83141A3E1 mov       eax,ebp
;             var v3 = Vector256.CreateScalarUnsafe(0f);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       7FF83141A3E3 vxorps    xmm0,xmm0,xmm0
;             var vectCnt = cnt / vectLen;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A3E7 mov       edx,eax
       7FF83141A3E9 sar       edx,1F
       7FF83141A3EC and       edx,7
       7FF83141A3EF add       edx,eax
       7FF83141A3F1 sar       edx,3
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
       7FF83141A3F4 vxorps    xmm1,xmm1,xmm1
;                 var ptr1 = (float*) Unsafe.AsPointer(ref span1[0]);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A3F8 cmp       ebp,0
       7FF83141A3FB jbe       near ptr M01_L19
       7FF83141A401 mov       rcx,rbx
;                 var ptr2 = (float*) Unsafe.AsPointer(ref span2[0]);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A404 cmp       r15d,0
       7FF83141A408 jbe       near ptr M01_L19
       7FF83141A40E mov       r8,r14
;                 for (i = 0; i < vectCnt; i++)
;                      ^^^^^
       7FF83141A411 xor       r9d,r9d
       7FF83141A414 test      edx,edx
       7FF83141A416 jle       short M01_L09
;                     var v1 = Avx.LoadVector256(ptr1);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF83141A418 vmovups   ymm2,[rcx]
;                     var v2 = Avx.LoadVector256(ptr2);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A41C vmovups   ymm3,[r8]
;                     v3 = Fma.MultiplyAdd(v1, v2, v3);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A421 vfmadd213ps ymm2,ymm3,ymm0
       7FF83141A426 vmovaps   ymm0,ymm2
;                     ptr1 += vectLen;
;                     ^^^^^^^^^^^^^^^^
       7FF83141A42A add       rcx,20
;                     ptr2 += vectLen;
;                     ^^^^^^^^^^^^^^^^
       7FF83141A42E add       r8,20
       7FF83141A432 inc       r9d
       7FF83141A435 cmp       r9d,edx
       7FF83141A438 jl        short M01_L08
;                 for (i = 0; i < vectLen; i++)
;                      ^^^^^
M01_L09:
       7FF83141A43A xor       r9d,r9d
;                     total += v3.GetElement(i);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       7FF83141A43D vmovupd   [rsp+20],ymm0
       7FF83141A443 cmp       r9d,8
       7FF83141A447 jae       near ptr M01_L18
       7FF83141A44D lea       rcx,[rsp+20]
       7FF83141A452 movsxd    r8,r9d
       7FF83141A455 vmovss    xmm2,dword ptr [rcx+r8*4]
       7FF83141A45B vaddss    xmm1,xmm1,xmm2
       7FF83141A45F inc       r9d
       7FF83141A462 cmp       r9d,8
       7FF83141A466 jl        short M01_L10
;                 i = vectCnt * vectLen;
;                 ^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A468 mov       r9d,edx
       7FF83141A46B shl       r9d,3
;                 if (cnt % vectLen > 0)
;                 ^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A46F mov       edx,eax
       7FF83141A471 sar       edx,1F
       7FF83141A474 and       edx,7
       7FF83141A477 add       edx,eax
       7FF83141A479 and       edx,0FFFFFFF8
       7FF83141A47C mov       ecx,eax
       7FF83141A47E sub       ecx,edx
       7FF83141A480 test      ecx,ecx
       7FF83141A482 jle       short M01_L12
;                     ptr1 = (float*) Unsafe.AsPointer(ref span1[i]);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A484 cmp       r9d,ebp
       7FF83141A487 jae       near ptr M01_L19
       7FF83141A48D movsxd    r8,r9d
       7FF83141A490 shl       r8,2
       7FF83141A494 lea       rcx,[rbx+r8]
;                     ptr2 = (float*) Unsafe.AsPointer(ref span2[i]);
;                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A498 cmp       r9d,r15d
       7FF83141A49B jae       near ptr M01_L19
       7FF83141A4A1 add       r8,r14
;                     for (; i < cnt; i++)
;                            ^^^^^^^
       7FF83141A4A4 cmp       r9d,eax
       7FF83141A4A7 jge       short M01_L12
;                         total += *ptr1++ * *ptr2++;
;                         ^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       7FF83141A4A9 mov       rdx,rcx
       7FF83141A4AC lea       rcx,[rdx+4]
       7FF83141A4B0 mov       r10,r8
       7FF83141A4B3 lea       r8,[r10+4]
       7FF83141A4B7 vmovss    xmm0,dword ptr [rdx]
       7FF83141A4BB vmulss    xmm0,xmm0,dword ptr [r10]
       7FF83141A4C0 vaddss    xmm1,xmm0,xmm1
       7FF83141A4C4 inc       r9d
       7FF83141A4C7 cmp       r9d,eax
       7FF83141A4CA jl        short M01_L11
M01_L12:
       7FF83141A4CC mov       edx,[rsi+0C]
       7FF83141A4CF cmp       edx,[rdi+0C]
       7FF83141A4D2 je        short M01_L16
       7FF83141A4D4 mov       edx,[rsi+0C]
       7FF83141A4D7 cmp       edx,[rdi+0C]
       7FF83141A4DA jg        short M01_L13
;                   for (var j = cnt; j < h.Length; j++)
;                        ^^^^^^^^^^^
       7FF83141A4DC jmp       short M01_L14
M01_L13:
       7FF83141A4DE mov       r14,rbx
       7FF83141A4E1 mov       r15d,ebp
M01_L14:
       7FF83141A4E4 cmp       eax,r15d
       7FF83141A4E7 jge       short M01_L16
;                     total += h[j];
;                     ^^^^^^^^^^^^^^
M01_L15:
       7FF83141A4E9 cmp       eax,r15d
       7FF83141A4EC jae       short M01_L19
       7FF83141A4EE movsxd    rdx,eax
       7FF83141A4F1 vaddss    xmm1,xmm1,dword ptr [r14+rdx*4]
       7FF83141A4F7 inc       eax
       7FF83141A4F9 cmp       eax,r15d
       7FF83141A4FC jl        short M01_L15
;             return total;
;             ^^^^^^^^^^^^^
M01_L16:
       7FF83141A4FE vmovaps   xmm0,xmm1
       7FF83141A502 vzeroupper
       7FF83141A505 add       rsp,68
       7FF83141A509 pop       rbx
       7FF83141A50A pop       rbp
       7FF83141A50B pop       rsi
       7FF83141A50C pop       rdi
       7FF83141A50D pop       r14
       7FF83141A50F pop       r15
       7FF83141A511 ret
M01_L17:
       7FF83141A512 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF83141A517 int       3
M01_L18:
       7FF83141A518 mov       ecx,15
       7FF83141A51D call      System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument)
       7FF83141A522 int       3
M01_L19:
       7FF83141A523 call      CORINFO_HELP_RNGCHKFAIL
       7FF83141A528 int       3
; Total bytes of code 569
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyWithVector()
;             return DotMultiplyIntrinsicWVector(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141FA90 cmp       [rcx],ecx
       7FF83141FA92 mov       [rsp+8],rcx
       7FF83141FA97 add       rcx,8
       7FF83141FA9B mov       rdx,[rsp+8]
       7FF83141FAA0 add       rdx,18
       7FF83141FAA4 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVector(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVector(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF83141A5E0 push      r14
       7FF83141A5E2 push      rdi
       7FF83141A5E3 push      rsi
       7FF83141A5E4 push      rbp
       7FF83141A5E5 push      rbx
       7FF83141A5E6 sub       rsp,60
       7FF83141A5EA vzeroupper
       7FF83141A5ED vxorps    xmm4,xmm4,xmm4
       7FF83141A5F1 vmovdqa   xmmword ptr [rsp+20],xmm4
       7FF83141A5F7 vmovdqa   xmmword ptr [rsp+30],xmm4
       7FF83141A5FD vmovdqa   xmmword ptr [rsp+40],xmm4
       7FF83141A603 vmovdqa   xmmword ptr [rsp+50],xmm4
       7FF83141A609 mov       rsi,rcx
       7FF83141A60C mov       rdi,rdx
;             var span1 = mem1.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A60F xor       ebx,ebx
       7FF83141A611 xor       ebp,ebp
       7FF83141A613 mov       rcx,[rsi]
       7FF83141A616 test      rcx,rcx
       7FF83141A619 je        short M01_L02
       7FF83141A61B mov       rdx,[rcx]
       7FF83141A61E test      dword ptr [rdx],80000000
       7FF83141A624 je        short M01_L00
       7FF83141A626 lea       rbx,[rcx+10]
       7FF83141A62A mov       ebp,[rcx+8]
       7FF83141A62D jmp       short M01_L01
M01_L00:
       7FF83141A62F lea       rdx,[rsp+50]
       7FF83141A634 mov       rax,[rcx]
       7FF83141A637 mov       rax,[rax+40]
       7FF83141A63B call      qword ptr [rax+28]
       7FF83141A63E mov       rbx,[rsp+50]
       7FF83141A643 mov       ebp,[rsp+58]
M01_L01:
       7FF83141A647 mov       edx,[rsi+8]
       7FF83141A64A and       edx,7FFFFFFF
       7FF83141A650 mov       ecx,[rsi+0C]
       7FF83141A653 mov       eax,ecx
       7FF83141A655 add       rax,rdx
       7FF83141A658 mov       r8d,ebp
       7FF83141A65B cmp       rax,r8
       7FF83141A65E ja        near ptr M01_L17
       7FF83141A664 lea       rbx,[rbx+rdx*4]
       7FF83141A668 mov       ebp,ecx
;             var span2 = mem2.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       7FF83141A66A xor       esi,esi
       7FF83141A66C xor       r14d,r14d
       7FF83141A66F mov       rcx,[rdi]
       7FF83141A672 test      rcx,rcx
       7FF83141A675 je        short M01_L05
       7FF83141A677 mov       rdx,[rcx]
       7FF83141A67A test      dword ptr [rdx],80000000
       7FF83141A680 je        short M01_L03
       7FF83141A682 lea       rsi,[rcx+10]
       7FF83141A686 mov       r14d,[rcx+8]
       7FF83141A68A jmp       short M01_L04
M01_L03:
       7FF83141A68C lea       rdx,[rsp+40]
       7FF83141A691 mov       rax,[rcx]
       7FF83141A694 mov       rax,[rax+40]
       7FF83141A698 call      qword ptr [rax+28]
       7FF83141A69B mov       rsi,[rsp+40]
       7FF83141A6A0 mov       r14d,[rsp+48]
M01_L04:
       7FF83141A6A5 mov       eax,[rdi+8]
       7FF83141A6A8 and       eax,7FFFFFFF
       7FF83141A6AD mov       edx,[rdi+0C]
       7FF83141A6B0 mov       ecx,edx
       7FF83141A6B2 add       rcx,rax
       7FF83141A6B5 mov       r8d,r14d
       7FF83141A6B8 cmp       rcx,r8
       7FF83141A6BB ja        near ptr M01_L17
       7FF83141A6C1 lea       rsi,[rsi+rax*4]
       7FF83141A6C5 mov       r14d,edx
M01_L05:
       7FF83141A6C8 mov       eax,ebp
       7FF83141A6CA cmp       eax,r14d
       7FF83141A6CD jle       short M01_L06
       7FF83141A6CF mov       eax,r14d
       7FF83141A6D2 jmp       short M01_L07
M01_L06:
       7FF83141A6D4 mov       eax,ebp
;             var v3 = Vector<float>.Zero;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       7FF83141A6D6 vxorps    ymm0,ymm0,ymm0
;             var vectCnt = cnt / vectLen;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A6DA mov       edx,eax
       7FF83141A6DC sar       edx,1F
       7FF83141A6DF and       edx,7
       7FF83141A6E2 add       edx,eax
       7FF83141A6E4 sar       edx,3
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
       7FF83141A6E7 vxorps    xmm1,xmm1,xmm1
;             for (i = 0; i < vectCnt; i++)
;                  ^^^^^
       7FF83141A6EB xor       ecx,ecx
       7FF83141A6ED test      edx,edx
       7FF83141A6EF jle       short M01_L09
;                 var index = i * vectLen;
;                 ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF83141A6F1 mov       r8d,ecx
       7FF83141A6F4 shl       r8d,3
;                 var v1 = new Vector<float>(span1.Slice(index));
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A6F8 cmp       r8d,ebp
       7FF83141A6FB ja        near ptr M01_L17
       7FF83141A701 mov       r9d,ebp
       7FF83141A704 sub       r9d,r8d
       7FF83141A707 movsxd    r10,r8d
       7FF83141A70A shl       r10,2
       7FF83141A70E lea       r11,[r10+rbx]
       7FF83141A712 cmp       r9d,8
       7FF83141A716 jl        near ptr M01_L18
       7FF83141A71C vmovupd   ymm2,[r11]
;                 var v2 = new Vector<float>(span2.Slice(index));
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A721 cmp       r8d,r14d
       7FF83141A724 ja        near ptr M01_L17
       7FF83141A72A mov       r9d,r14d
       7FF83141A72D sub       r9d,r8d
       7FF83141A730 add       r10,rsi
       7FF83141A733 cmp       r9d,8
       7FF83141A737 jl        near ptr M01_L18
       7FF83141A73D vmovupd   ymm3,[r10]
;                 v3 += v1 * v2;
;                 ^^^^^^^^^^^^^^
       7FF83141A742 vmulps    ymm2,ymm2,ymm3
       7FF83141A746 vaddps    ymm0,ymm0,ymm2
       7FF83141A74A inc       ecx
       7FF83141A74C cmp       ecx,edx
       7FF83141A74E jl        short M01_L08
;             for (i = 0; i < vectLen; i++)
;                  ^^^^^
M01_L09:
       7FF83141A750 xor       ecx,ecx
;                 total += v3[i];
;                 ^^^^^^^^^^^^^^^
M01_L10:
       7FF83141A752 vmovupd   [rsp+20],ymm0
       7FF83141A758 vmovss    xmm2,dword ptr [rsp+rcx*4+20]
       7FF83141A75E vaddss    xmm1,xmm2,xmm1
       7FF83141A762 inc       ecx
       7FF83141A764 cmp       ecx,8
       7FF83141A767 jl        short M01_L10
;             for (i = vectCnt * vectLen; i < cnt; i++)
;                  ^^^^^^^^^^^^^^^^^^^^^
       7FF83141A769 mov       ecx,edx
       7FF83141A76B shl       ecx,3
       7FF83141A76E cmp       ecx,eax
       7FF83141A770 jge       short M01_L12
;                 total += span1[i] * span2[i];
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       7FF83141A772 cmp       ecx,ebp
       7FF83141A774 jae       short M01_L19
       7FF83141A776 movsxd    rdx,ecx
       7FF83141A779 vmovss    xmm0,dword ptr [rbx+rdx*4]
       7FF83141A77E cmp       ecx,r14d
       7FF83141A781 jae       short M01_L19
       7FF83141A783 movsxd    rdx,ecx
       7FF83141A786 vmulss    xmm0,xmm0,dword ptr [rsi+rdx*4]
       7FF83141A78B vaddss    xmm1,xmm0,xmm1
       7FF83141A78F inc       ecx
       7FF83141A791 cmp       ecx,eax
       7FF83141A793 jl        short M01_L11
M01_L12:
       7FF83141A795 cmp       ebp,r14d
       7FF83141A798 je        short M01_L16
       7FF83141A79A cmp       ebp,r14d
       7FF83141A79D jg        short M01_L13
;                 for (var j = cnt; j < h.Length; j++)
;                      ^^^^^^^^^^^
       7FF83141A79F jmp       short M01_L14
M01_L13:
       7FF83141A7A1 mov       rsi,rbx
       7FF83141A7A4 mov       r14d,ebp
M01_L14:
       7FF83141A7A7 cmp       eax,r14d
       7FF83141A7AA jge       short M01_L16
;                     total += h[j];
;                     ^^^^^^^^^^^^^^
M01_L15:
       7FF83141A7AC cmp       eax,r14d
       7FF83141A7AF jae       short M01_L19
       7FF83141A7B1 movsxd    rdx,eax
       7FF83141A7B4 vaddss    xmm1,xmm1,dword ptr [rsi+rdx*4]
       7FF83141A7B9 inc       eax
       7FF83141A7BB cmp       eax,r14d
       7FF83141A7BE jl        short M01_L15
;             return total;
;             ^^^^^^^^^^^^^
M01_L16:
       7FF83141A7C0 vmovaps   xmm0,xmm1
       7FF83141A7C4 vzeroupper
       7FF83141A7C7 add       rsp,60
       7FF83141A7CB pop       rbx
       7FF83141A7CC pop       rbp
       7FF83141A7CD pop       rsi
       7FF83141A7CE pop       rdi
       7FF83141A7CF pop       r14
       7FF83141A7D1 ret
M01_L17:
       7FF83141A7D2 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF83141A7D7 int       3
M01_L18:
       7FF83141A7D8 mov       ecx,8
       7FF83141A7DD call      System.Numerics.Vector.ThrowInsufficientNumberOfElementsException(Int32)
       7FF83141A7E2 int       3
M01_L19:
       7FF83141A7E3 call      CORINFO_HELP_RNGCHKFAIL
       7FF83141A7E8 int       3
; Total bytes of code 521
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyWithVectorDot()
;             return DotMultiplyIntrinsicWVectorDot(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141FA50 cmp       [rcx],ecx
       7FF83141FA52 mov       [rsp+8],rcx
       7FF83141FA57 add       rcx,8
       7FF83141FA5B mov       rdx,[rsp+8]
       7FF83141FA60 add       rdx,18
       7FF83141FA64 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVectorDot(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVectorDot(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF83141A5E0 push      r14
       7FF83141A5E2 push      rdi
       7FF83141A5E3 push      rsi
       7FF83141A5E4 push      rbp
       7FF83141A5E5 push      rbx
       7FF83141A5E6 sub       rsp,40
       7FF83141A5EA vzeroupper
       7FF83141A5ED vxorps    xmm4,xmm4,xmm4
       7FF83141A5F1 vmovdqa   xmmword ptr [rsp+20],xmm4
       7FF83141A5F7 vmovdqa   xmmword ptr [rsp+30],xmm4
       7FF83141A5FD mov       rsi,rcx
       7FF83141A600 mov       rdi,rdx
;             var span1 = mem1.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A603 xor       ebx,ebx
       7FF83141A605 xor       ebp,ebp
       7FF83141A607 mov       rcx,[rsi]
       7FF83141A60A test      rcx,rcx
       7FF83141A60D je        short M01_L02
       7FF83141A60F mov       rdx,[rcx]
       7FF83141A612 test      dword ptr [rdx],80000000
       7FF83141A618 je        short M01_L00
       7FF83141A61A lea       rbx,[rcx+10]
       7FF83141A61E mov       ebp,[rcx+8]
       7FF83141A621 jmp       short M01_L01
M01_L00:
       7FF83141A623 lea       rdx,[rsp+30]
       7FF83141A628 mov       rax,[rcx]
       7FF83141A62B mov       rax,[rax+40]
       7FF83141A62F call      qword ptr [rax+28]
       7FF83141A632 mov       rbx,[rsp+30]
       7FF83141A637 mov       ebp,[rsp+38]
M01_L01:
       7FF83141A63B mov       edx,[rsi+8]
       7FF83141A63E and       edx,7FFFFFFF
       7FF83141A644 mov       ecx,[rsi+0C]
       7FF83141A647 mov       eax,ecx
       7FF83141A649 add       rax,rdx
       7FF83141A64C mov       r8d,ebp
       7FF83141A64F cmp       rax,r8
       7FF83141A652 ja        near ptr M01_L16
       7FF83141A658 lea       rbx,[rbx+rdx*4]
       7FF83141A65C mov       ebp,ecx
;             var span2 = mem2.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       7FF83141A65E xor       esi,esi
       7FF83141A660 xor       r14d,r14d
       7FF83141A663 mov       rcx,[rdi]
       7FF83141A666 test      rcx,rcx
       7FF83141A669 je        short M01_L05
       7FF83141A66B mov       rdx,[rcx]
       7FF83141A66E test      dword ptr [rdx],80000000
       7FF83141A674 je        short M01_L03
       7FF83141A676 lea       rsi,[rcx+10]
       7FF83141A67A mov       r14d,[rcx+8]
       7FF83141A67E jmp       short M01_L04
M01_L03:
       7FF83141A680 lea       rdx,[rsp+20]
       7FF83141A685 mov       rax,[rcx]
       7FF83141A688 mov       rax,[rax+40]
       7FF83141A68C call      qword ptr [rax+28]
       7FF83141A68F mov       rsi,[rsp+20]
       7FF83141A694 mov       r14d,[rsp+28]
M01_L04:
       7FF83141A699 mov       eax,[rdi+8]
       7FF83141A69C and       eax,7FFFFFFF
       7FF83141A6A1 mov       edx,[rdi+0C]
       7FF83141A6A4 mov       ecx,edx
       7FF83141A6A6 add       rcx,rax
       7FF83141A6A9 mov       r8d,r14d
       7FF83141A6AC cmp       rcx,r8
       7FF83141A6AF ja        near ptr M01_L16
       7FF83141A6B5 lea       rsi,[rsi+rax*4]
       7FF83141A6B9 mov       r14d,edx
M01_L05:
       7FF83141A6BC mov       eax,ebp
       7FF83141A6BE cmp       eax,r14d
       7FF83141A6C1 jle       short M01_L06
       7FF83141A6C3 mov       eax,r14d
       7FF83141A6C6 jmp       short M01_L07
M01_L06:
       7FF83141A6C8 mov       eax,ebp
;             var vectLen = Vector<float>.Count;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       7FF83141A6CA mov       edx,eax
       7FF83141A6CC sar       edx,1F
       7FF83141A6CF and       edx,7
       7FF83141A6D2 add       edx,eax
       7FF83141A6D4 sar       edx,3
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
       7FF83141A6D7 vxorps    xmm0,xmm0,xmm0
;             for (i = 0; i < vectCnt; i++)
;                  ^^^^^
       7FF83141A6DB xor       ecx,ecx
       7FF83141A6DD test      edx,edx
       7FF83141A6DF jle       short M01_L09
;                 var index = i * vectLen;
;                 ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF83141A6E1 mov       r8d,ecx
       7FF83141A6E4 shl       r8d,3
;                 var v1 = new Vector<float>(span1.Slice(index));
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A6E8 cmp       r8d,ebp
       7FF83141A6EB ja        near ptr M01_L16
       7FF83141A6F1 mov       r9d,ebp
       7FF83141A6F4 sub       r9d,r8d
       7FF83141A6F7 movsxd    r10,r8d
       7FF83141A6FA shl       r10,2
       7FF83141A6FE lea       r11,[r10+rbx]
       7FF83141A702 cmp       r9d,8
       7FF83141A706 jl        near ptr M01_L17
       7FF83141A70C vmovupd   ymm1,[r11]
;                 var v2 = new Vector<float>(span2.Slice(index));
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A711 cmp       r8d,r14d
       7FF83141A714 ja        near ptr M01_L16
       7FF83141A71A mov       r9d,r14d
       7FF83141A71D sub       r9d,r8d
       7FF83141A720 add       r10,rsi
       7FF83141A723 cmp       r9d,8
       7FF83141A727 jl        near ptr M01_L17
       7FF83141A72D vmovupd   ymm2,[r10]
;                 total += Vector.Dot(v1, v2);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83141A732 vdpps     ymm1,ymm1,ymm2,0F1
       7FF83141A738 vextractf128 xmm2,ymm1,1
       7FF83141A73E vaddps    xmm1,xmm1,xmm2
       7FF83141A742 vaddss    xmm0,xmm1,xmm0
       7FF83141A746 inc       ecx
       7FF83141A748 cmp       ecx,edx
       7FF83141A74A jl        short M01_L08
;             for (i = vectCnt * vectLen; i < cnt; i++)
;                  ^^^^^^^^^^^^^^^^^^^^^
M01_L09:
       7FF83141A74C mov       ecx,edx
       7FF83141A74E shl       ecx,3
       7FF83141A751 cmp       ecx,eax
       7FF83141A753 jge       short M01_L11
;                 total += span1[i] * span2[i];
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       7FF83141A755 cmp       ecx,ebp
       7FF83141A757 jae       short M01_L18
       7FF83141A759 movsxd    rdx,ecx
       7FF83141A75C vmovss    xmm1,dword ptr [rbx+rdx*4]
       7FF83141A761 cmp       ecx,r14d
       7FF83141A764 jae       short M01_L18
       7FF83141A766 movsxd    rdx,ecx
       7FF83141A769 vmulss    xmm1,xmm1,dword ptr [rsi+rdx*4]
       7FF83141A76E vaddss    xmm0,xmm1,xmm0
       7FF83141A772 inc       ecx
       7FF83141A774 cmp       ecx,eax
       7FF83141A776 jl        short M01_L10
M01_L11:
       7FF83141A778 cmp       ebp,r14d
       7FF83141A77B je        short M01_L15
       7FF83141A77D cmp       ebp,r14d
       7FF83141A780 jg        short M01_L12
;                 for (var j = cnt; j < h.Length; j++)
;                      ^^^^^^^^^^^
       7FF83141A782 jmp       short M01_L13
M01_L12:
       7FF83141A784 mov       rsi,rbx
       7FF83141A787 mov       r14d,ebp
M01_L13:
       7FF83141A78A cmp       eax,r14d
       7FF83141A78D jge       short M01_L15
;                     total += h[j];
;                     ^^^^^^^^^^^^^^
M01_L14:
       7FF83141A78F cmp       eax,r14d
       7FF83141A792 jae       short M01_L18
       7FF83141A794 movsxd    rdx,eax
       7FF83141A797 vaddss    xmm0,xmm0,dword ptr [rsi+rdx*4]
       7FF83141A79C inc       eax
       7FF83141A79E cmp       eax,r14d
       7FF83141A7A1 jl        short M01_L14
M01_L15:
       7FF83141A7A3 vzeroupper
       7FF83141A7A6 add       rsp,40
       7FF83141A7AA pop       rbx
       7FF83141A7AB pop       rbp
       7FF83141A7AC pop       rsi
       7FF83141A7AD pop       rdi
       7FF83141A7AE pop       r14
       7FF83141A7B0 ret
M01_L16:
       7FF83141A7B1 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF83141A7B6 int       3
M01_L17:
       7FF83141A7B7 mov       ecx,8
       7FF83141A7BC call      System.Numerics.Vector.ThrowInsufficientNumberOfElementsException(Int32)
       7FF83141A7C1 int       3
M01_L18:
       7FF83141A7C2 call      CORINFO_HELP_RNGCHKFAIL
       7FF83141A7C7 int       3
; Total bytes of code 488
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyWithVectorMul()
;             return DotMultiplyIntrinsicWVectorMul(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831440130 cmp       [rcx],ecx
       7FF831440132 mov       [rsp+8],rcx
       7FF831440137 add       rcx,8
       7FF83144013B mov       rdx,[rsp+8]
       7FF831440140 add       rdx,18
       7FF831440144 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVectorMul(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyIntrinsicWVectorMul(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF83143A850 push      r14
       7FF83143A852 push      rdi
       7FF83143A853 push      rsi
       7FF83143A854 push      rbp
       7FF83143A855 push      rbx
       7FF83143A856 sub       rsp,60
       7FF83143A85A vzeroupper
       7FF83143A85D vxorps    xmm4,xmm4,xmm4
       7FF83143A861 vmovdqa   xmmword ptr [rsp+20],xmm4
       7FF83143A867 vmovdqa   xmmword ptr [rsp+30],xmm4
       7FF83143A86D vmovdqa   xmmword ptr [rsp+40],xmm4
       7FF83143A873 vmovdqa   xmmword ptr [rsp+50],xmm4
       7FF83143A879 mov       rsi,rcx
       7FF83143A87C mov       rdi,rdx
;             var span1 = mem1.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^
       7FF83143A87F xor       ebx,ebx
       7FF83143A881 xor       ebp,ebp
       7FF83143A883 mov       rcx,[rsi]
       7FF83143A886 test      rcx,rcx
       7FF83143A889 je        short M01_L02
       7FF83143A88B mov       rdx,[rcx]
       7FF83143A88E test      dword ptr [rdx],80000000
       7FF83143A894 je        short M01_L00
       7FF83143A896 lea       rbx,[rcx+10]
       7FF83143A89A mov       ebp,[rcx+8]
       7FF83143A89D jmp       short M01_L01
M01_L00:
       7FF83143A89F lea       rdx,[rsp+50]
       7FF83143A8A4 mov       rax,[rcx]
       7FF83143A8A7 mov       rax,[rax+40]
       7FF83143A8AB call      qword ptr [rax+28]
       7FF83143A8AE mov       rbx,[rsp+50]
       7FF83143A8B3 mov       ebp,[rsp+58]
M01_L01:
       7FF83143A8B7 mov       edx,[rsi+8]
       7FF83143A8BA and       edx,7FFFFFFF
       7FF83143A8C0 mov       ecx,[rsi+0C]
       7FF83143A8C3 mov       eax,ecx
       7FF83143A8C5 add       rax,rdx
       7FF83143A8C8 mov       r8d,ebp
       7FF83143A8CB cmp       rax,r8
       7FF83143A8CE ja        near ptr M01_L17
       7FF83143A8D4 lea       rbx,[rbx+rdx*4]
       7FF83143A8D8 mov       ebp,ecx
;             var span2 = mem2.Span;
;             ^^^^^^^^^^^^^^^^^^^^^^
M01_L02:
       7FF83143A8DA xor       esi,esi
       7FF83143A8DC xor       r14d,r14d
       7FF83143A8DF mov       rcx,[rdi]
       7FF83143A8E2 test      rcx,rcx
       7FF83143A8E5 je        short M01_L05
       7FF83143A8E7 mov       rdx,[rcx]
       7FF83143A8EA test      dword ptr [rdx],80000000
       7FF83143A8F0 je        short M01_L03
       7FF83143A8F2 lea       rsi,[rcx+10]
       7FF83143A8F6 mov       r14d,[rcx+8]
       7FF83143A8FA jmp       short M01_L04
M01_L03:
       7FF83143A8FC lea       rdx,[rsp+40]
       7FF83143A901 mov       rax,[rcx]
       7FF83143A904 mov       rax,[rax+40]
       7FF83143A908 call      qword ptr [rax+28]
       7FF83143A90B mov       rsi,[rsp+40]
       7FF83143A910 mov       r14d,[rsp+48]
M01_L04:
       7FF83143A915 mov       eax,[rdi+8]
       7FF83143A918 and       eax,7FFFFFFF
       7FF83143A91D mov       edx,[rdi+0C]
       7FF83143A920 mov       ecx,edx
       7FF83143A922 add       rcx,rax
       7FF83143A925 mov       r8d,r14d
       7FF83143A928 cmp       rcx,r8
       7FF83143A92B ja        near ptr M01_L17
       7FF83143A931 lea       rsi,[rsi+rax*4]
       7FF83143A935 mov       r14d,edx
M01_L05:
       7FF83143A938 mov       eax,ebp
       7FF83143A93A cmp       eax,r14d
       7FF83143A93D jle       short M01_L06
       7FF83143A93F mov       eax,r14d
       7FF83143A942 jmp       short M01_L07
M01_L06:
       7FF83143A944 mov       eax,ebp
;             var v3 = Vector<float>.Zero;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L07:
       7FF83143A946 vxorps    ymm0,ymm0,ymm0
;             var vectCnt = cnt / vectLen;
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83143A94A mov       edx,eax
       7FF83143A94C sar       edx,1F
       7FF83143A94F and       edx,7
       7FF83143A952 add       edx,eax
       7FF83143A954 sar       edx,3
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
       7FF83143A957 vxorps    xmm1,xmm1,xmm1
;             for (i = 0; i < vectCnt; i++)
;                  ^^^^^
       7FF83143A95B xor       ecx,ecx
       7FF83143A95D test      edx,edx
       7FF83143A95F jle       short M01_L09
;                 var index = i * vectLen;
;                 ^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF83143A961 mov       r8d,ecx
       7FF83143A964 shl       r8d,3
;                 var v1 = new Vector<float>(span1.Slice(index));
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83143A968 cmp       r8d,ebp
       7FF83143A96B ja        near ptr M01_L17
       7FF83143A971 mov       r9d,ebp
       7FF83143A974 sub       r9d,r8d
       7FF83143A977 movsxd    r10,r8d
       7FF83143A97A shl       r10,2
       7FF83143A97E lea       r11,[r10+rbx]
       7FF83143A982 cmp       r9d,8
       7FF83143A986 jl        near ptr M01_L18
       7FF83143A98C vmovupd   ymm2,[r11]
;                 var v2 = new Vector<float>(span2.Slice(index));
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83143A991 cmp       r8d,r14d
       7FF83143A994 ja        near ptr M01_L17
       7FF83143A99A mov       r9d,r14d
       7FF83143A99D sub       r9d,r8d
       7FF83143A9A0 add       r10,rsi
       7FF83143A9A3 cmp       r9d,8
       7FF83143A9A7 jl        near ptr M01_L18
       7FF83143A9AD vmovupd   ymm3,[r10]
       7FF83143A9B2 vmulps    ymm2,ymm2,ymm3
       7FF83143A9B6 vaddps    ymm0,ymm0,ymm2
       7FF83143A9BA inc       ecx
       7FF83143A9BC cmp       ecx,edx
       7FF83143A9BE jl        short M01_L08
;             for (i = 0; i < vectLen; i++)
;                  ^^^^^
M01_L09:
       7FF83143A9C0 xor       ecx,ecx
;                 total += v3[i];
;                 ^^^^^^^^^^^^^^^
M01_L10:
       7FF83143A9C2 vmovupd   [rsp+20],ymm0
       7FF83143A9C8 vmovss    xmm2,dword ptr [rsp+rcx*4+20]
       7FF83143A9CE vaddss    xmm1,xmm2,xmm1
       7FF83143A9D2 inc       ecx
       7FF83143A9D4 cmp       ecx,8
       7FF83143A9D7 jl        short M01_L10
;             for (i = vectCnt * vectLen; i < cnt; i++)
;                  ^^^^^^^^^^^^^^^^^^^^^
       7FF83143A9D9 mov       ecx,edx
       7FF83143A9DB shl       ecx,3
       7FF83143A9DE cmp       ecx,eax
       7FF83143A9E0 jge       short M01_L12
;                 total += span1[i] * span2[i];
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L11:
       7FF83143A9E2 cmp       ecx,ebp
       7FF83143A9E4 jae       short M01_L19
       7FF83143A9E6 movsxd    rdx,ecx
       7FF83143A9E9 vmovss    xmm0,dword ptr [rbx+rdx*4]
       7FF83143A9EE cmp       ecx,r14d
       7FF83143A9F1 jae       short M01_L19
       7FF83143A9F3 movsxd    rdx,ecx
       7FF83143A9F6 vmulss    xmm0,xmm0,dword ptr [rsi+rdx*4]
       7FF83143A9FB vaddss    xmm1,xmm0,xmm1
       7FF83143A9FF inc       ecx
       7FF83143AA01 cmp       ecx,eax
       7FF83143AA03 jl        short M01_L11
M01_L12:
       7FF83143AA05 cmp       ebp,r14d
       7FF83143AA08 je        short M01_L16
       7FF83143AA0A cmp       ebp,r14d
       7FF83143AA0D jg        short M01_L13
;                 for (var j = cnt; j < h.Length; j++)
;                      ^^^^^^^^^^^
       7FF83143AA0F jmp       short M01_L14
M01_L13:
       7FF83143AA11 mov       rsi,rbx
       7FF83143AA14 mov       r14d,ebp
M01_L14:
       7FF83143AA17 cmp       eax,r14d
       7FF83143AA1A jge       short M01_L16
;                     total += h[j];
;                     ^^^^^^^^^^^^^^
M01_L15:
       7FF83143AA1C cmp       eax,r14d
       7FF83143AA1F jae       short M01_L19
       7FF83143AA21 movsxd    rdx,eax
       7FF83143AA24 vaddss    xmm1,xmm1,dword ptr [rsi+rdx*4]
       7FF83143AA29 inc       eax
       7FF83143AA2B cmp       eax,r14d
       7FF83143AA2E jl        short M01_L15
;             return total;
;             ^^^^^^^^^^^^^
M01_L16:
       7FF83143AA30 vmovaps   xmm0,xmm1
       7FF83143AA34 vzeroupper
       7FF83143AA37 add       rsp,60
       7FF83143AA3B pop       rbx
       7FF83143AA3C pop       rbp
       7FF83143AA3D pop       rsi
       7FF83143AA3E pop       rdi
       7FF83143AA3F pop       r14
       7FF83143AA41 ret
M01_L17:
       7FF83143AA42 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF83143AA47 int       3
M01_L18:
       7FF83143AA48 mov       ecx,8
       7FF83143AA4D call      System.Numerics.Vector.ThrowInsufficientNumberOfElementsException(Int32)
       7FF83143AA52 int       3
M01_L19:
       7FF83143AA53 call      CORINFO_HELP_RNGCHKFAIL
       7FF83143AA58 int       3
; Total bytes of code 521
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyClassicSingle()
;             return DotMultiplyClassic(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83143F1E0 cmp       [rcx],ecx
       7FF83143F1E2 mov       [rsp+8],rcx
       7FF83143F1E7 add       rcx,8
       7FF83143F1EB mov       rdx,[rsp+8]
       7FF83143F1F0 add       rdx,18
       7FF83143F1F4 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyClassic(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyClassic(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF83143A1E0 push      r15
       7FF83143A1E2 push      r14
       7FF83143A1E4 push      r12
       7FF83143A1E6 push      rdi
       7FF83143A1E7 push      rsi
       7FF83143A1E8 push      rbp
       7FF83143A1E9 push      rbx
       7FF83143A1EA sub       rsp,40
       7FF83143A1EE vzeroupper
       7FF83143A1F1 vxorps    xmm4,xmm4,xmm4
       7FF83143A1F5 vmovdqa   xmmword ptr [rsp+20],xmm4
       7FF83143A1FB vmovdqa   xmmword ptr [rsp+30],xmm4
       7FF83143A201 mov       esi,[rcx+0C]
       7FF83143A204 mov       eax,esi
       7FF83143A206 mov       edi,[rdx+0C]
       7FF83143A209 cmp       eax,edi
       7FF83143A20B jg        short M01_L00
;             var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83143A20D mov       rax,[rdx]
       7FF83143A210 mov       ebx,[rdx+8]
       7FF83143A213 mov       rbp,[rcx]
       7FF83143A216 mov       r14d,[rcx+8]
       7FF83143A21A jmp       short M01_L01
;             var hs = hv.Span;
;             ^^^^^^^^^^^^^^^^^
M01_L00:
       7FF83143A21C mov       rax,[rcx]
       7FF83143A21F mov       ebx,[rcx+8]
       7FF83143A222 mov       ecx,esi
       7FF83143A224 mov       rbp,[rdx]
       7FF83143A227 mov       r14d,[rdx+8]
       7FF83143A22B mov       esi,edi
       7FF83143A22D mov       edi,ecx
M01_L01:
       7FF83143A22F xor       r15d,r15d
       7FF83143A232 xor       r12d,r12d
       7FF83143A235 test      rax,rax
       7FF83143A238 je        short M01_L04
       7FF83143A23A mov       rdx,[rax]
       7FF83143A23D test      dword ptr [rdx],80000000
       7FF83143A243 je        short M01_L02
       7FF83143A245 lea       r15,[rax+10]
       7FF83143A249 mov       r12d,[rax+8]
       7FF83143A24D jmp       short M01_L03
M01_L02:
       7FF83143A24F lea       rdx,[rsp+30]
       7FF83143A254 mov       rcx,rax
       7FF83143A257 mov       rax,[rax]
       7FF83143A25A mov       rax,[rax+40]
       7FF83143A25E call      qword ptr [rax+28]
       7FF83143A261 mov       r15,[rsp+30]
       7FF83143A266 mov       r12d,[rsp+38]
M01_L03:
       7FF83143A26B and       ebx,7FFFFFFF
       7FF83143A271 mov       edx,ebx
       7FF83143A273 mov       ecx,edi
       7FF83143A275 add       rcx,rdx
       7FF83143A278 mov       eax,r12d
       7FF83143A27B cmp       rcx,rax
       7FF83143A27E ja        near ptr M01_L12
       7FF83143A284 lea       r15,[r15+rdx*4]
       7FF83143A288 mov       r12d,edi
;             var ls = lv.Span;
;             ^^^^^^^^^^^^^^^^^
M01_L04:
       7FF83143A28B xor       edi,edi
       7FF83143A28D xor       ebx,ebx
       7FF83143A28F mov       rcx,rbp
       7FF83143A292 test      rcx,rcx
       7FF83143A295 je        short M01_L07
       7FF83143A297 mov       rdx,[rcx]
       7FF83143A29A test      dword ptr [rdx],80000000
       7FF83143A2A0 je        short M01_L05
       7FF83143A2A2 lea       rdi,[rcx+10]
       7FF83143A2A6 mov       ebx,[rcx+8]
       7FF83143A2A9 jmp       short M01_L06
M01_L05:
       7FF83143A2AB lea       rdx,[rsp+20]
       7FF83143A2B0 mov       rax,[rcx]
       7FF83143A2B3 mov       rax,[rax+40]
       7FF83143A2B7 call      qword ptr [rax+28]
       7FF83143A2BA mov       rdi,[rsp+20]
       7FF83143A2BF mov       ebx,[rsp+28]
M01_L06:
       7FF83143A2C3 and       r14d,7FFFFFFF
       7FF83143A2CA mov       eax,r14d
       7FF83143A2CD mov       edx,esi
       7FF83143A2CF add       rdx,rax
       7FF83143A2D2 mov       ecx,ebx
       7FF83143A2D4 cmp       rdx,rcx
       7FF83143A2D7 ja        short M01_L12
       7FF83143A2D9 lea       rdi,[rdi+rax*4]
       7FF83143A2DD mov       ebx,esi
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
M01_L07:
       7FF83143A2DF vxorps    xmm0,xmm0,xmm0
;             var i = 0;
;             ^^^^^^^^^^
       7FF83143A2E3 xor       eax,eax
       7FF83143A2E5 test      ebx,ebx
       7FF83143A2E7 jle       short M01_L09
;                 total += hs[i] * ls[i];
;                 ^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF83143A2E9 cmp       eax,r12d
       7FF83143A2EC jae       short M01_L13
       7FF83143A2EE movsxd    rdx,eax
       7FF83143A2F1 vmovss    xmm1,dword ptr [r15+rdx*4]
       7FF83143A2F7 vmulss    xmm1,xmm1,dword ptr [rdi+rdx*4]
       7FF83143A2FC vaddss    xmm0,xmm1,xmm0
;             for (; i < ls.Length; i++)
;                                   ^^^
       7FF83143A300 inc       eax
       7FF83143A302 cmp       eax,ebx
       7FF83143A304 jl        short M01_L08
M01_L09:
       7FF83143A306 cmp       eax,r12d
       7FF83143A309 jge       short M01_L11
;                 total += hs[i];
;                 ^^^^^^^^^^^^^^^
M01_L10:
       7FF83143A30B cmp       eax,r12d
       7FF83143A30E jae       short M01_L13
       7FF83143A310 movsxd    rdx,eax
       7FF83143A313 vaddss    xmm0,xmm0,dword ptr [r15+rdx*4]
;             for (; i < hs.Length; i++)
;                                   ^^^
       7FF83143A319 inc       eax
       7FF83143A31B cmp       eax,r12d
       7FF83143A31E jl        short M01_L10
M01_L11:
       7FF83143A320 add       rsp,40
       7FF83143A324 pop       rbx
       7FF83143A325 pop       rbp
       7FF83143A326 pop       rsi
       7FF83143A327 pop       rdi
       7FF83143A328 pop       r12
       7FF83143A32A pop       r14
       7FF83143A32C pop       r15
       7FF83143A32E ret
M01_L12:
       7FF83143A32F call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF83143A334 int       3
M01_L13:
       7FF83143A335 call      CORINFO_HELP_RNGCHKFAIL
       7FF83143A33A int       3
; Total bytes of code 347
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyClassicSingleWPtr()
;             return DotMultiplyClassicWPtr(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142EF60 cmp       [rcx],ecx
       7FF83142EF62 mov       [rsp+8],rcx
       7FF83142EF67 add       rcx,8
       7FF83142EF6B mov       rdx,[rsp+8]
       7FF83142EF70 add       rdx,18
       7FF83142EF74 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyClassicWPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyClassicWPtr(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF831429D90 push      r15
       7FF831429D92 push      r14
       7FF831429D94 push      r12
       7FF831429D96 push      rdi
       7FF831429D97 push      rsi
       7FF831429D98 push      rbp
       7FF831429D99 push      rbx
       7FF831429D9A sub       rsp,50
       7FF831429D9E vzeroupper
       7FF831429DA1 vmovaps   [rsp+40],xmm6
       7FF831429DA7 vxorps    xmm4,xmm4,xmm4
       7FF831429DAB vmovdqa   xmmword ptr [rsp+20],xmm4
       7FF831429DB1 vmovdqa   xmmword ptr [rsp+30],xmm4
       7FF831429DB7 mov       esi,[rcx+0C]
       7FF831429DBA mov       eax,esi
       7FF831429DBC mov       edi,[rdx+0C]
       7FF831429DBF cmp       eax,edi
       7FF831429DC1 jg        short M01_L00
;             var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831429DC3 mov       rax,[rdx]
       7FF831429DC6 mov       ebx,[rdx+8]
       7FF831429DC9 mov       rbp,[rcx]
       7FF831429DCC mov       r14d,[rcx+8]
       7FF831429DD0 jmp       short M01_L01
;             var hl = hv.Length;
;             ^^^^^^^^^^^^^^^^^^^
M01_L00:
       7FF831429DD2 mov       rax,[rcx]
       7FF831429DD5 mov       ebx,[rcx+8]
       7FF831429DD8 mov       ecx,esi
       7FF831429DDA mov       rbp,[rdx]
       7FF831429DDD mov       r14d,[rdx+8]
       7FF831429DE1 mov       esi,edi
       7FF831429DE3 mov       edi,ecx
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
M01_L01:
       7FF831429DE5 vxorps    xmm6,xmm6,xmm6
;                   var hs = (float*) Unsafe.AsPointer(ref hv.Span[0]);
;                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831429DE9 xor       r15d,r15d
       7FF831429DEC xor       r12d,r12d
       7FF831429DEF test      rax,rax
       7FF831429DF2 je        short M01_L04
       7FF831429DF4 mov       rdx,[rax]
       7FF831429DF7 test      dword ptr [rdx],80000000
       7FF831429DFD je        short M01_L02
       7FF831429DFF lea       r15,[rax+10]
       7FF831429E03 mov       r12d,[rax+8]
       7FF831429E07 jmp       short M01_L03
M01_L02:
       7FF831429E09 lea       rdx,[rsp+30]
       7FF831429E0E mov       rcx,rax
       7FF831429E11 mov       rax,[rax]
       7FF831429E14 mov       rax,[rax+40]
       7FF831429E18 call      qword ptr [rax+28]
       7FF831429E1B mov       r15,[rsp+30]
       7FF831429E20 mov       r12d,[rsp+38]
M01_L03:
       7FF831429E25 and       ebx,7FFFFFFF
       7FF831429E2B mov       edx,ebx
       7FF831429E2D mov       ecx,edi
       7FF831429E2F add       rcx,rdx
       7FF831429E32 mov       eax,r12d
       7FF831429E35 cmp       rcx,rax
       7FF831429E38 ja        near ptr M01_L14
       7FF831429E3E lea       r15,[r15+rdx*4]
       7FF831429E42 mov       r12d,edi
M01_L04:
       7FF831429E45 cmp       r12d,0
       7FF831429E49 jbe       near ptr M01_L15
;                 var ls = (float*) Unsafe.AsPointer(ref lv.Span[0]);
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831429E4F xor       ebx,ebx
       7FF831429E51 xor       r12d,r12d
       7FF831429E54 mov       rcx,rbp
       7FF831429E57 test      rcx,rcx
       7FF831429E5A je        short M01_L07
       7FF831429E5C mov       rdx,[rcx]
       7FF831429E5F test      dword ptr [rdx],80000000
       7FF831429E65 je        short M01_L05
       7FF831429E67 lea       rbx,[rcx+10]
       7FF831429E6B mov       r12d,[rcx+8]
       7FF831429E6F jmp       short M01_L06
M01_L05:
       7FF831429E71 lea       rdx,[rsp+20]
       7FF831429E76 mov       rax,[rcx]
       7FF831429E79 mov       rax,[rax+40]
       7FF831429E7D call      qword ptr [rax+28]
       7FF831429E80 mov       rbx,[rsp+20]
       7FF831429E85 mov       r12d,[rsp+28]
M01_L06:
       7FF831429E8A and       r14d,7FFFFFFF
       7FF831429E91 mov       eax,r14d
       7FF831429E94 mov       edx,esi
       7FF831429E96 add       rdx,rax
       7FF831429E99 mov       ecx,r12d
       7FF831429E9C cmp       rdx,rcx
       7FF831429E9F ja        short M01_L14
       7FF831429EA1 lea       rbx,[rbx+rax*4]
       7FF831429EA5 mov       r12d,esi
M01_L07:
       7FF831429EA8 cmp       r12d,0
       7FF831429EAC jbe       short M01_L15
;                 var i = 0;
;                 ^^^^^^^^^^
       7FF831429EAE xor       eax,eax
;                 for (; i < ll; i++)
;                        ^^^^^^
       7FF831429EB0 test      esi,esi
       7FF831429EB2 jle       short M01_L13
;                     total += *hs++ * *ls++;
;                     ^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF831429EB4 lea       rdx,[r15+4]
       7FF831429EB8 mov       rbp,rdx
       7FF831429EBB lea       rdx,[rbx+4]
       7FF831429EBF vmovss    xmm0,dword ptr [r15]
       7FF831429EC4 vmulss    xmm0,xmm0,dword ptr [rbx]
       7FF831429EC8 vaddss    xmm6,xmm0,xmm6
       7FF831429ECC inc       eax
       7FF831429ECE cmp       eax,esi
       7FF831429ED0 mov       rbx,rdx
       7FF831429ED3 jl        short M01_L12
;                 for (; i < hl; i++)
;                        ^^^^^^
M01_L09:
       7FF831429ED5 cmp       eax,edi
       7FF831429ED7 jge       short M01_L11
;                     total += *hs++;
;                     ^^^^^^^^^^^^^^^
M01_L10:
       7FF831429ED9 mov       rdx,rbp
       7FF831429EDC lea       rbp,[rdx+4]
       7FF831429EE0 vaddss    xmm6,xmm6,dword ptr [rdx]
       7FF831429EE4 inc       eax
       7FF831429EE6 cmp       eax,edi
       7FF831429EE8 jl        short M01_L10
;             return total;
;             ^^^^^^^^^^^^^
M01_L11:
       7FF831429EEA vmovaps   xmm0,xmm6
       7FF831429EEE vmovaps   xmm6,[rsp+40]
       7FF831429EF4 add       rsp,50
       7FF831429EF8 pop       rbx
       7FF831429EF9 pop       rbp
       7FF831429EFA pop       rsi
       7FF831429EFB pop       rdi
       7FF831429EFC pop       r12
       7FF831429EFE pop       r14
       7FF831429F00 pop       r15
       7FF831429F02 ret
M01_L12:
       7FF831429F03 mov       r15,rbp
       7FF831429F06 jmp       short M01_L08
M01_L13:
       7FF831429F08 mov       rbp,r15
       7FF831429F0B jmp       short M01_L09
M01_L14:
       7FF831429F0D call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF831429F12 int       3
M01_L15:
       7FF831429F13 call      CORINFO_HELP_RNGCHKFAIL
       7FF831429F18 int       3
; Total bytes of code 393
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyClassicGroup4()
;             return DotMultiplyClassicGroup4(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142ED40 cmp       [rcx],ecx
       7FF83142ED42 mov       [rsp+8],rcx
       7FF83142ED47 add       rcx,8
       7FF83142ED4B mov       rdx,[rsp+8]
       7FF83142ED50 add       rdx,18
       7FF83142ED54 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyClassicGroup4(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyClassicGroup4(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF831429C80 push      r15
       7FF831429C82 push      r14
       7FF831429C84 push      r12
       7FF831429C86 push      rdi
       7FF831429C87 push      rsi
       7FF831429C88 push      rbp
       7FF831429C89 push      rbx
       7FF831429C8A sub       rsp,40
       7FF831429C8E vzeroupper
       7FF831429C91 vxorps    xmm4,xmm4,xmm4
       7FF831429C95 vmovdqa   xmmword ptr [rsp+20],xmm4
       7FF831429C9B vmovdqa   xmmword ptr [rsp+30],xmm4
       7FF831429CA1 mov       esi,[rcx+0C]
       7FF831429CA4 mov       eax,esi
       7FF831429CA6 mov       edi,[rdx+0C]
       7FF831429CA9 cmp       eax,edi
       7FF831429CAB jg        short M01_L00
;             var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF831429CAD mov       rax,[rdx]
       7FF831429CB0 mov       ebx,[rdx+8]
       7FF831429CB3 mov       rbp,[rcx]
       7FF831429CB6 mov       r14d,[rcx+8]
       7FF831429CBA jmp       short M01_L01
;             var hs = hv.Span;
;             ^^^^^^^^^^^^^^^^^
M01_L00:
       7FF831429CBC mov       rax,[rcx]
       7FF831429CBF mov       ebx,[rcx+8]
       7FF831429CC2 mov       ecx,esi
       7FF831429CC4 mov       rbp,[rdx]
       7FF831429CC7 mov       r14d,[rdx+8]
       7FF831429CCB mov       esi,edi
       7FF831429CCD mov       edi,ecx
M01_L01:
       7FF831429CCF xor       r15d,r15d
       7FF831429CD2 xor       r12d,r12d
       7FF831429CD5 test      rax,rax
       7FF831429CD8 je        short M01_L04
       7FF831429CDA mov       rdx,[rax]
       7FF831429CDD test      dword ptr [rdx],80000000
       7FF831429CE3 je        short M01_L02
       7FF831429CE5 lea       r15,[rax+10]
       7FF831429CE9 mov       r12d,[rax+8]
       7FF831429CED jmp       short M01_L03
M01_L02:
       7FF831429CEF lea       rdx,[rsp+30]
       7FF831429CF4 mov       rcx,rax
       7FF831429CF7 mov       rax,[rax]
       7FF831429CFA mov       rax,[rax+40]
       7FF831429CFE call      qword ptr [rax+28]
       7FF831429D01 mov       r15,[rsp+30]
       7FF831429D06 mov       r12d,[rsp+38]
M01_L03:
       7FF831429D0B and       ebx,7FFFFFFF
       7FF831429D11 mov       edx,ebx
       7FF831429D13 mov       ecx,edi
       7FF831429D15 add       rcx,rdx
       7FF831429D18 mov       eax,r12d
       7FF831429D1B cmp       rcx,rax
       7FF831429D1E ja        near ptr M01_L15
       7FF831429D24 lea       r15,[r15+rdx*4]
       7FF831429D28 mov       r12d,edi
;             var ls = lv.Span;
;             ^^^^^^^^^^^^^^^^^
M01_L04:
       7FF831429D2B xor       edi,edi
       7FF831429D2D xor       ebx,ebx
       7FF831429D2F mov       rcx,rbp
       7FF831429D32 test      rcx,rcx
       7FF831429D35 je        short M01_L07
       7FF831429D37 mov       rdx,[rcx]
       7FF831429D3A test      dword ptr [rdx],80000000
       7FF831429D40 je        short M01_L05
       7FF831429D42 lea       rdi,[rcx+10]
       7FF831429D46 mov       ebx,[rcx+8]
       7FF831429D49 jmp       short M01_L06
M01_L05:
       7FF831429D4B lea       rdx,[rsp+20]
       7FF831429D50 mov       rax,[rcx]
       7FF831429D53 mov       rax,[rax+40]
       7FF831429D57 call      qword ptr [rax+28]
       7FF831429D5A mov       rdi,[rsp+20]
       7FF831429D5F mov       ebx,[rsp+28]
M01_L06:
       7FF831429D63 and       r14d,7FFFFFFF
       7FF831429D6A mov       eax,r14d
       7FF831429D6D mov       edx,esi
       7FF831429D6F add       rdx,rax
       7FF831429D72 mov       ecx,ebx
       7FF831429D74 cmp       rdx,rcx
       7FF831429D77 ja        near ptr M01_L15
       7FF831429D7D lea       rdi,[rdi+rax*4]
       7FF831429D81 mov       ebx,esi
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
M01_L07:
       7FF831429D83 vxorps    xmm0,xmm0,xmm0
;             var i = 0;
;             ^^^^^^^^^^
       7FF831429D87 xor       eax,eax
       7FF831429D89 test      ebx,ebx
       7FF831429D8B jle       near ptr M01_L09
;                 total += hs[i] * ls[i] + hs[i + 1] * ls[i + 1] + hs[i + 2] * ls[i + 2] + hs[i + 3] * ls[i + 3];
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF831429D91 cmp       eax,r12d
       7FF831429D94 jae       near ptr M01_L16
       7FF831429D9A movsxd    rdx,eax
       7FF831429D9D vmovss    xmm1,dword ptr [r15+rdx*4]
       7FF831429DA3 cmp       eax,ebx
       7FF831429DA5 jae       near ptr M01_L16
       7FF831429DAB vmulss    xmm1,xmm1,dword ptr [rdi+rdx*4]
       7FF831429DB0 lea       edx,[rax+1]
       7FF831429DB3 cmp       edx,r12d
       7FF831429DB6 jae       near ptr M01_L16
       7FF831429DBC movsxd    rcx,edx
       7FF831429DBF vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF831429DC5 cmp       edx,ebx
       7FF831429DC7 jae       near ptr M01_L16
       7FF831429DCD vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF831429DD2 vaddss    xmm1,xmm1,xmm2
       7FF831429DD6 lea       edx,[rax+2]
       7FF831429DD9 cmp       edx,r12d
       7FF831429DDC jae       near ptr M01_L16
       7FF831429DE2 movsxd    rcx,edx
       7FF831429DE5 vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF831429DEB cmp       edx,ebx
       7FF831429DED jae       near ptr M01_L16
       7FF831429DF3 vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF831429DF8 vaddss    xmm1,xmm1,xmm2
       7FF831429DFC lea       edx,[rax+3]
       7FF831429DFF cmp       edx,r12d
       7FF831429E02 jae       near ptr M01_L16
       7FF831429E08 movsxd    rcx,edx
       7FF831429E0B vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF831429E11 cmp       edx,ebx
       7FF831429E13 jae       near ptr M01_L16
       7FF831429E19 vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF831429E1E vaddss    xmm1,xmm1,xmm2
       7FF831429E22 vaddss    xmm0,xmm1,xmm0
;             for (; i < ls.Length; i += grp)
;                                   ^^^^^^^^
       7FF831429E26 add       eax,4
       7FF831429E29 cmp       eax,ebx
       7FF831429E2B jl        near ptr M01_L08
M01_L09:
       7FF831429E31 mov       edx,ebx
       7FF831429E33 sar       edx,1F
       7FF831429E36 and       edx,3
       7FF831429E39 add       edx,ebx
       7FF831429E3B and       edx,0FFFFFFFC
       7FF831429E3E mov       ecx,ebx
       7FF831429E40 sub       ecx,edx
       7FF831429E42 je        short M01_L13
;                 for (i += grp; i < ls.Length; i++)
;                      ^^^^^^^^
       7FF831429E44 add       eax,4
       7FF831429E47 cmp       eax,ebx
       7FF831429E49 jge       short M01_L11
;                     total += hs[i] * ls[i];
;                     ^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       7FF831429E4B cmp       eax,r12d
       7FF831429E4E jae       short M01_L16
       7FF831429E50 movsxd    rdx,eax
       7FF831429E53 vmovss    xmm1,dword ptr [r15+rdx*4]
       7FF831429E59 cmp       eax,ebx
       7FF831429E5B jae       short M01_L16
       7FF831429E5D vmulss    xmm1,xmm1,dword ptr [rdi+rdx*4]
       7FF831429E62 vaddss    xmm0,xmm1,xmm0
       7FF831429E66 inc       eax
       7FF831429E68 cmp       eax,ebx
       7FF831429E6A jl        short M01_L10
M01_L11:
       7FF831429E6C cmp       eax,r12d
       7FF831429E6F jge       short M01_L14
;                 total += hs[i];
;                 ^^^^^^^^^^^^^^^
M01_L12:
       7FF831429E71 cmp       eax,r12d
       7FF831429E74 jae       short M01_L16
       7FF831429E76 movsxd    rdx,eax
       7FF831429E79 vaddss    xmm0,xmm0,dword ptr [r15+rdx*4]
;             for (; i < hs.Length; i++)
;                                   ^^^
       7FF831429E7F inc       eax
M01_L13:
       7FF831429E81 cmp       eax,r12d
       7FF831429E84 jl        short M01_L12
M01_L14:
       7FF831429E86 add       rsp,40
       7FF831429E8A pop       rbx
       7FF831429E8B pop       rbp
       7FF831429E8C pop       rsi
       7FF831429E8D pop       rdi
       7FF831429E8E pop       r12
       7FF831429E90 pop       r14
       7FF831429E92 pop       r15
       7FF831429E94 ret
M01_L15:
       7FF831429E95 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF831429E9A int       3
M01_L16:
       7FF831429E9B call      CORINFO_HELP_RNGCHKFAIL
       7FF831429EA0 int       3
; Total bytes of code 545
```

## .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```assembly
; Benchmarker.Benchmarker.MultiplyClassicGroup8()
;             return DotMultiplyClassicGroup8(ref m_Ma1, ref m_Ma2);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142F360 cmp       [rcx],ecx
       7FF83142F362 mov       [rsp+8],rcx
       7FF83142F367 add       rcx,8
       7FF83142F36B mov       rdx,[rsp+8]
       7FF83142F370 add       rdx,18
       7FF83142F374 jmp       near ptr CoreNN.Tools.Multipliers.DotMultiplyClassicGroup8(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
; Total bytes of code 25
```
```assembly
; CoreNN.Tools.Multipliers.DotMultiplyClassicGroup8(System.Memory`1<Single> ByRef, System.Memory`1<Single> ByRef)
       7FF83142A1E0 push      r15
       7FF83142A1E2 push      r14
       7FF83142A1E4 push      r12
       7FF83142A1E6 push      rdi
       7FF83142A1E7 push      rsi
       7FF83142A1E8 push      rbp
       7FF83142A1E9 push      rbx
       7FF83142A1EA sub       rsp,40
       7FF83142A1EE vzeroupper
       7FF83142A1F1 vxorps    xmm4,xmm4,xmm4
       7FF83142A1F5 vmovdqa   xmmword ptr [rsp+20],xmm4
       7FF83142A1FB vmovdqa   xmmword ptr [rsp+30],xmm4
       7FF83142A201 mov       esi,[rcx+0C]
       7FF83142A204 mov       eax,esi
       7FF83142A206 mov       edi,[rdx+0C]
       7FF83142A209 cmp       eax,edi
       7FF83142A20B jg        short M01_L00
;             var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
;             ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
       7FF83142A20D mov       rax,[rdx]
       7FF83142A210 mov       ebx,[rdx+8]
       7FF83142A213 mov       rbp,[rcx]
       7FF83142A216 mov       r14d,[rcx+8]
       7FF83142A21A jmp       short M01_L01
;             var hs = hv.Span;
;             ^^^^^^^^^^^^^^^^^
M01_L00:
       7FF83142A21C mov       rax,[rcx]
       7FF83142A21F mov       ebx,[rcx+8]
       7FF83142A222 mov       ecx,esi
       7FF83142A224 mov       rbp,[rdx]
       7FF83142A227 mov       r14d,[rdx+8]
       7FF83142A22B mov       esi,edi
       7FF83142A22D mov       edi,ecx
M01_L01:
       7FF83142A22F xor       r15d,r15d
       7FF83142A232 xor       r12d,r12d
       7FF83142A235 test      rax,rax
       7FF83142A238 je        short M01_L04
       7FF83142A23A mov       rdx,[rax]
       7FF83142A23D test      dword ptr [rdx],80000000
       7FF83142A243 je        short M01_L02
       7FF83142A245 lea       r15,[rax+10]
       7FF83142A249 mov       r12d,[rax+8]
       7FF83142A24D jmp       short M01_L03
M01_L02:
       7FF83142A24F lea       rdx,[rsp+30]
       7FF83142A254 mov       rcx,rax
       7FF83142A257 mov       rax,[rax]
       7FF83142A25A mov       rax,[rax+40]
       7FF83142A25E call      qword ptr [rax+28]
       7FF83142A261 mov       r15,[rsp+30]
       7FF83142A266 mov       r12d,[rsp+38]
M01_L03:
       7FF83142A26B and       ebx,7FFFFFFF
       7FF83142A271 mov       edx,ebx
       7FF83142A273 mov       ecx,edi
       7FF83142A275 add       rcx,rdx
       7FF83142A278 mov       eax,r12d
       7FF83142A27B cmp       rcx,rax
       7FF83142A27E ja        near ptr M01_L14
       7FF83142A284 lea       r15,[r15+rdx*4]
       7FF83142A288 mov       r12d,edi
;             var ls = lv.Span;
;             ^^^^^^^^^^^^^^^^^
M01_L04:
       7FF83142A28B xor       edi,edi
       7FF83142A28D xor       ebx,ebx
       7FF83142A28F mov       rcx,rbp
       7FF83142A292 test      rcx,rcx
       7FF83142A295 je        short M01_L07
       7FF83142A297 mov       rdx,[rcx]
       7FF83142A29A test      dword ptr [rdx],80000000
       7FF83142A2A0 je        short M01_L05
       7FF83142A2A2 lea       rdi,[rcx+10]
       7FF83142A2A6 mov       ebx,[rcx+8]
       7FF83142A2A9 jmp       short M01_L06
M01_L05:
       7FF83142A2AB lea       rdx,[rsp+20]
       7FF83142A2B0 mov       rax,[rcx]
       7FF83142A2B3 mov       rax,[rax+40]
       7FF83142A2B7 call      qword ptr [rax+28]
       7FF83142A2BA mov       rdi,[rsp+20]
       7FF83142A2BF mov       ebx,[rsp+28]
M01_L06:
       7FF83142A2C3 and       r14d,7FFFFFFF
       7FF83142A2CA mov       eax,r14d
       7FF83142A2CD mov       edx,esi
       7FF83142A2CF add       rdx,rax
       7FF83142A2D2 mov       ecx,ebx
       7FF83142A2D4 cmp       rdx,rcx
       7FF83142A2D7 ja        near ptr M01_L14
       7FF83142A2DD lea       rdi,[rdi+rax*4]
       7FF83142A2E1 mov       ebx,esi
;             var total = 0f;
;             ^^^^^^^^^^^^^^^
M01_L07:
       7FF83142A2E3 vxorps    xmm0,xmm0,xmm0
;             var i = 0;
;             ^^^^^^^^^^
       7FF83142A2E7 xor       eax,eax
       7FF83142A2E9 test      ebx,ebx
       7FF83142A2EB jle       near ptr M01_L09
;                 total += hs[i] * ls[i] + hs[i + 1] * ls[i + 1] + hs[i + 2] * ls[i + 2] + hs[i + 3] * ls[i + 3]
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
;                          + hs[i + 4] * ls[i + 4] + hs[i + 5] * ls[i + 5] + hs[i + 6] * ls[i + 6] + hs[i + 7] * ls[i + 7];
;                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
M01_L08:
       7FF83142A2F1 cmp       eax,r12d
       7FF83142A2F4 jae       near ptr M01_L15
       7FF83142A2FA movsxd    rdx,eax
       7FF83142A2FD vmovss    xmm1,dword ptr [r15+rdx*4]
       7FF83142A303 cmp       eax,ebx
       7FF83142A305 jae       near ptr M01_L15
       7FF83142A30B vmulss    xmm1,xmm1,dword ptr [rdi+rdx*4]
       7FF83142A310 lea       edx,[rax+1]
       7FF83142A313 cmp       edx,r12d
       7FF83142A316 jae       near ptr M01_L15
       7FF83142A31C movsxd    rcx,edx
       7FF83142A31F vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF83142A325 cmp       edx,ebx
       7FF83142A327 jae       near ptr M01_L15
       7FF83142A32D vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF83142A332 vaddss    xmm1,xmm1,xmm2
       7FF83142A336 lea       edx,[rax+2]
       7FF83142A339 cmp       edx,r12d
       7FF83142A33C jae       near ptr M01_L15
       7FF83142A342 movsxd    rcx,edx
       7FF83142A345 vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF83142A34B cmp       edx,ebx
       7FF83142A34D jae       near ptr M01_L15
       7FF83142A353 vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF83142A358 vaddss    xmm1,xmm1,xmm2
       7FF83142A35C lea       edx,[rax+3]
       7FF83142A35F cmp       edx,r12d
       7FF83142A362 jae       near ptr M01_L15
       7FF83142A368 movsxd    rcx,edx
       7FF83142A36B vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF83142A371 cmp       edx,ebx
       7FF83142A373 jae       near ptr M01_L15
       7FF83142A379 vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF83142A37E vaddss    xmm1,xmm1,xmm2
       7FF83142A382 lea       edx,[rax+4]
       7FF83142A385 cmp       edx,r12d
       7FF83142A388 jae       near ptr M01_L15
       7FF83142A38E movsxd    rcx,edx
       7FF83142A391 vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF83142A397 cmp       edx,ebx
       7FF83142A399 jae       near ptr M01_L15
       7FF83142A39F vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF83142A3A4 vaddss    xmm1,xmm1,xmm2
       7FF83142A3A8 lea       edx,[rax+5]
       7FF83142A3AB cmp       edx,r12d
       7FF83142A3AE jae       near ptr M01_L15
       7FF83142A3B4 movsxd    rcx,edx
       7FF83142A3B7 vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF83142A3BD cmp       edx,ebx
       7FF83142A3BF jae       near ptr M01_L15
       7FF83142A3C5 vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF83142A3CA vaddss    xmm1,xmm1,xmm2
       7FF83142A3CE lea       edx,[rax+6]
       7FF83142A3D1 cmp       edx,r12d
       7FF83142A3D4 jae       near ptr M01_L15
       7FF83142A3DA movsxd    rcx,edx
       7FF83142A3DD vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF83142A3E3 cmp       edx,ebx
       7FF83142A3E5 jae       near ptr M01_L15
       7FF83142A3EB vmulss    xmm2,xmm2,dword ptr [rdi+rcx*4]
       7FF83142A3F0 vaddss    xmm1,xmm1,xmm2
       7FF83142A3F4 lea       edx,[rax+7]
       7FF83142A3F7 cmp       edx,r12d
       7FF83142A3FA jae       near ptr M01_L15
       7FF83142A400 movsxd    rcx,edx
       7FF83142A403 vmovss    xmm2,dword ptr [r15+rcx*4]
       7FF83142A409 cmp       edx,ebx
       7FF83142A40B jae       near ptr M01_L15
       7FF83142A411 vmovss    xmm3,dword ptr [rdi+rcx*4]
       7FF83142A416 vmulss    xmm2,xmm2,xmm3
       7FF83142A41A vaddss    xmm1,xmm2,xmm1
       7FF83142A41E vaddss    xmm0,xmm0,xmm1
;             for (; i < ls.Length; i += grp)
;                                   ^^^^^^^^
       7FF83142A422 add       eax,8
       7FF83142A425 cmp       eax,ebx
       7FF83142A427 jl        near ptr M01_L08
M01_L09:
       7FF83142A42D mov       edx,ebx
       7FF83142A42F sar       edx,1F
       7FF83142A432 and       edx,7
       7FF83142A435 add       edx,ebx
       7FF83142A437 and       edx,0FFFFFFF8
       7FF83142A43A mov       ecx,ebx
       7FF83142A43C sub       ecx,edx
       7FF83142A43E je        short M01_L11
       7FF83142A440 mov       edx,ebx
;                 for (i += grp; i < len; i++)
;                      ^^^^^^^^
       7FF83142A442 add       eax,8
       7FF83142A445 cmp       eax,ebx
       7FF83142A447 jge       short M01_L11
;                     total += hs[i] * ls[i];
;                     ^^^^^^^^^^^^^^^^^^^^^^^
M01_L10:
       7FF83142A449 cmp       eax,r12d
       7FF83142A44C jae       short M01_L15
       7FF83142A44E movsxd    rcx,eax
       7FF83142A451 vmovss    xmm1,dword ptr [r15+rcx*4]
       7FF83142A457 cmp       eax,edx
       7FF83142A459 jae       short M01_L15
       7FF83142A45B vmulss    xmm1,xmm1,dword ptr [rdi+rcx*4]
       7FF83142A460 vaddss    xmm0,xmm1,xmm0
       7FF83142A464 inc       eax
       7FF83142A466 cmp       eax,ebx
       7FF83142A468 jl        short M01_L10
M01_L11:
       7FF83142A46A mov       edx,r12d
;             for (; i < ln; i++)
;                    ^^^^^^
       7FF83142A46D cmp       eax,r12d
       7FF83142A470 jge       short M01_L13
;                 total += hs[i];
;                 ^^^^^^^^^^^^^^^
M01_L12:
       7FF83142A472 cmp       eax,edx
       7FF83142A474 jae       short M01_L15
       7FF83142A476 movsxd    rcx,eax
       7FF83142A479 vaddss    xmm0,xmm0,dword ptr [r15+rcx*4]
       7FF83142A47F inc       eax
       7FF83142A481 cmp       eax,r12d
       7FF83142A484 jl        short M01_L12
M01_L13:
       7FF83142A486 add       rsp,40
       7FF83142A48A pop       rbx
       7FF83142A48B pop       rbp
       7FF83142A48C pop       rsi
       7FF83142A48D pop       rdi
       7FF83142A48E pop       r12
       7FF83142A490 pop       r14
       7FF83142A492 pop       r15
       7FF83142A494 ret
M01_L14:
       7FF83142A495 call      System.ThrowHelper.ThrowArgumentOutOfRangeException()
       7FF83142A49A int       3
M01_L15:
       7FF83142A49B call      CORINFO_HELP_RNGCHKFAIL
       7FF83142A4A0 int       3
; Total bytes of code 705
```

