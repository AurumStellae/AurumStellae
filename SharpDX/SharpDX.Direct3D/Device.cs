﻿// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

namespace SharpDX.Direct3D9
{
	[InteropPatch]
	public unsafe class Device : ComObject
	{
		public Device(IntPtr nativePtr) : base(nativePtr) {
		}
		
		public Device(Direct3D direct3D, int adapter, DeviceType deviceType, IntPtr hFocusWindow, CreateFlags behaviorFlags, params PresentParameters[] presentationParametersRef) {
			direct3D.CreateDevice(adapter, deviceType, hFocusWindow, behaviorFlags, presentationParametersRef, this);
		}
		
		public void DrawIndexedUserPrimitives<S, T>(PrimitiveType type, int minimumVertexIndex, int vertexCount, int primitiveCount, S[] indexData, Format indexDataFormat, T[] vertexData)
			where S : struct
			where T : struct {
			DrawIndexedUserPrimitives(type, 0, 0, minimumVertexIndex, vertexCount, primitiveCount, indexData, indexDataFormat, vertexData);
		}

		public void DrawIndexedUserPrimitives<S, T>(PrimitiveType type, int startIndex, int minimumVertexIndex, int vertexCount, int primitiveCount, S[] indexData, Format indexDataFormat, T[] vertexData)
			where S : struct
			where T : struct {
			DrawIndexedUserPrimitives(type, startIndex, 0, minimumVertexIndex, vertexCount, primitiveCount, indexData, indexDataFormat, vertexData);
		}

		public void DrawIndexedUserPrimitives<S, T>(PrimitiveType type, int startIndex, int startVertex, int minimumVertexIndex, int vertexCount, int primitiveCount, S[] indexData, Format indexDataFormat, T[] vertexData)
			where S : struct
			where T : struct {
			DrawIndexedPrimitiveUP(type, minimumVertexIndex, vertexCount, primitiveCount, (IntPtr)Interop.Fixed(ref indexData[startIndex]), indexDataFormat, (IntPtr)Interop.Fixed(ref vertexData[startVertex]), Interop.SizeOf<T>());
		}

		public void Present() {
			Present(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
		}
		
		public Result TestCooperativeLevel() {
			return Interop.Calli(comPointer,(*(IntPtr**)comPointer)[3]);
		}
		
		public uint AvailableTextureMemory {
			get { return (uint)Interop.Calli(comPointer,(*(IntPtr**)comPointer)[4]); }
		}
		
		public void EvictManagedResources() {
			Result res = Interop.Calli(comPointer,(*(IntPtr**)comPointer)[5]);
			res.CheckError();
		}
		
		public Capabilities Capabilities {
			get {
				Capabilities capsRef = new Capabilities();
				Result res = Interop.Calli(comPointer, (IntPtr)(void*)&capsRef,(*(IntPtr**)comPointer)[7]);
				res.CheckError();
				return capsRef;
			}
		}
		
		public DisplayMode GetDisplayMode(int iSwapChain) {
			DisplayMode modeRef = new DisplayMode();
			Result res = Interop.Calli(comPointer, iSwapChain, (IntPtr)(void*)&modeRef,(*(IntPtr**)comPointer)[8]);
			res.CheckError();
			return modeRef;
		}
		
		public CreationParameters CreationParameters {
			get {
				CreationParameters parametersRef = new CreationParameters();
				Result res = Interop.Calli(comPointer, (IntPtr)(void*)&parametersRef,(*(IntPtr**)comPointer)[9]);
				res.CheckError();
				return parametersRef;
			}
		}
		
		public void Reset(params PresentParameters[] presentationParametersRef) {
			Result res;
			fixed (void* presentParamsRef = presentationParametersRef)
				res= Interop.Calli(comPointer, (IntPtr)presentParamsRef,(*(IntPtr**)comPointer)[16]);
			res.CheckError();
		}
		
		internal void Present(IntPtr sourceRectRef, IntPtr destRectRef, IntPtr hDestWindowOverride, IntPtr dirtyRegionRef) {
			Result res = Interop.Calli(comPointer, sourceRectRef, destRectRef, hDestWindowOverride, dirtyRegionRef,(*(IntPtr**)comPointer)[17]);
			res.CheckError();
		}
		
		public Surface GetBackBuffer(int iSwapChain, int iBackBuffer, BackBufferType type) {
			IntPtr backBufferOut = IntPtr.Zero;
			Result res = Interop.Calli(comPointer, iSwapChain, iBackBuffer, (int)type, (IntPtr)(void*)&backBufferOut,(*(IntPtr**)comPointer)[18]);
			res.CheckError();
			return ( backBufferOut == IntPtr.Zero ) ? null : new Surface( backBufferOut );
		}
		
		internal void CreateTexture(int width, int height, int levels, Usage usage, Format format, Pool pool, Texture textureOut) {
			IntPtr pOut = IntPtr.Zero;
			Result res = Interop.Calli(comPointer, width, height, levels, (int)usage, (int)format, (int)pool, (IntPtr)(void*)&pOut, IntPtr.Zero,(*(IntPtr**)comPointer)[23]);
			textureOut.comPointer = pOut;
			res.CheckError();
		}
		
		internal void CreateVertexBuffer(int length, Usage usage, VertexFormat vertexFormat, Pool pool, VertexBuffer vertexBufferOut) {
			IntPtr pOut = IntPtr.Zero;
			Result res = Interop.Calli(comPointer, length, (int)usage, (int)vertexFormat, (int)pool, (IntPtr)(void*)&pOut,IntPtr.Zero,(*(IntPtr**)comPointer)[26]);
			vertexBufferOut.comPointer = pOut;
			res.CheckError();
		}
		
		internal void CreateIndexBuffer(int length, int usage, Format format, Pool pool, IndexBuffer indexBufferOut) {
			IntPtr pOut = IntPtr.Zero;
			Result res = Interop.Calli(comPointer, length, usage, (int)format, (int)pool, (IntPtr)(void*)&pOut, IntPtr.Zero,(*(IntPtr**)comPointer)[27]);
			indexBufferOut.comPointer = pOut;
			res.CheckError();
		}
		
		public void GetRenderTargetData(Surface renderTarget, Surface destSurface) {
			Result res = Interop.Calli(comPointer, renderTarget.comPointer, destSurface.comPointer,(*(IntPtr**)comPointer)[32]);
			res.CheckError();
		}
		
		public Surface CreateOffscreenPlainSurface(int width, int height, Format format, Pool pool) {
			IntPtr pOut = IntPtr.Zero;
			Result res = Interop.Calli(comPointer, width, height, (int)format, (int)pool, (IntPtr)(void*)&pOut, IntPtr.Zero,(*(IntPtr**)comPointer)[36]);
			res.CheckError();
			return (pOut == IntPtr.Zero) ? null : new Surface(pOut);
		}

		public void BeginScene() {
			Result res = Interop.Calli(comPointer,(*(IntPtr**)comPointer)[41]);
			res.CheckError();
		}

		public void EndScene() {
			Result res = Interop.Calli(comPointer,(*(IntPtr**)comPointer)[42]);
			res.CheckError();
		}
		
		public void Clear(ClearFlags flags, int colorBGRA, float z, int stencil) {
			Result res = Interop.Calli(comPointer, 0, IntPtr.Zero, (int)flags, colorBGRA, z, stencil, (*(IntPtr**)comPointer)[43]);
			res.CheckError();
		}

		public void SetTransform(TransformState state, ref Matrix matrixRef) {
			Result res;
			fixed (void* matrixRef_ = &matrixRef)
				res= Interop.Calli(comPointer, (int)state, (IntPtr)matrixRef_,(*(IntPtr**)comPointer)[44]);
			res.CheckError();
		}
		
		public void SetRenderState(RenderState renderState, bool enable) {
			SetRenderState(renderState, enable ? 1 : 0);
		}

		public void SetRenderState(RenderState renderState, float value) {
			SetRenderState(renderState, *(int*)&value);
		}

		public void SetRenderState(RenderState state, int value) {
			Result res = Interop.Calli(comPointer, (int)state, value,(*(IntPtr**)comPointer)[57]);
			res.CheckError();
		}
		
		public void SetTexture(int stage, Texture texture) {
			Result res = Interop.Calli(comPointer, stage, (texture == null)?IntPtr.Zero:texture.comPointer,(*(IntPtr**)comPointer)[65]);
			res.CheckError();
		}
		
		public void SetTextureStageState(int stage, TextureStage type, int value) {
			Result res = Interop.Calli(comPointer, stage, (int)type, value,(*(IntPtr**)comPointer)[67]);
			res.CheckError();
		}
		
		public void DrawPrimitives(PrimitiveType type, int startVertex, int primitiveCount) {
			Result res = Interop.Calli(comPointer,(int)type, startVertex, primitiveCount,(*(IntPtr**)comPointer)[81]);
			res.CheckError();
		}
		
		public void DrawIndexedPrimitives(PrimitiveType type, int baseVertexIndex, int minVertexIndex, int numVertices, int startIndex, int primCount) {
			Result res = Interop.Calli(comPointer, (int)type, baseVertexIndex, minVertexIndex, numVertices, startIndex, primCount,(*(IntPtr**)comPointer)[82]);
			res.CheckError();
		}
		
		public void DrawUserPrimitives<T>(PrimitiveType type, int startIndex, int primitiveCount, T[] data) where T : struct {
			DrawPrimitiveUP(type, primitiveCount, (IntPtr)Interop.Fixed(ref data[startIndex]), Interop.SizeOf<T>());
		}
		
		internal void DrawPrimitiveUP(PrimitiveType type, int primitiveCount, IntPtr dataRef, int stride) {
			Result res = Interop.Calli(comPointer, (int)type, primitiveCount, dataRef, stride,(*(IntPtr**)comPointer)[83]);
			res.CheckError();
		}
		
		internal void DrawIndexedPrimitiveUP(PrimitiveType type, int minVertexIndex, int numVertices, int primitiveCount,
		                                     IntPtr indexDataRef, Format indexDataFormat, IntPtr dataRef, int stride) {
			Result res = Interop.Calli(comPointer, (int)type, minVertexIndex, numVertices, primitiveCount, indexDataRef, (int)indexDataFormat, dataRef, stride,(*(IntPtr**)comPointer)[84]);
			res.CheckError();
		}

		public void SetVertexFormat(VertexFormat vertexFormat) {
			Result res = Interop.Calli(comPointer, (int)vertexFormat,(*(IntPtr**)comPointer)[89]);
			res.CheckError();
		}
		
		public void SetStreamSource(int streamNumber, VertexBuffer streamData, int offsetInBytes, int stride) {
			Result res = Interop.Calli(comPointer, streamNumber,(streamData == null)?IntPtr.Zero:streamData.comPointer,offsetInBytes, stride,(*(IntPtr**)comPointer)[100]);
			res.CheckError();
		}
		
		public void SetIndices(IndexBuffer indexData) {
			Result res = Interop.Calli(comPointer,(indexData == null)?IntPtr.Zero:indexData.comPointer,(*(IntPtr**)comPointer)[104]);
			res.CheckError();
		}
	}
}
