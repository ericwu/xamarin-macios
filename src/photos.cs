using XamCore.ObjCRuntime;
using XamCore.Foundation;
using XamCore.CoreFoundation;
using XamCore.CoreLocation;
using XamCore.UIKit;
using XamCore.AVFoundation;
using XamCore.CoreGraphics;
using System;

namespace XamCore.Photos
{
	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHAdjustmentData : NSCoding, NSSecureCoding {

		[Export ("initWithFormatIdentifier:formatVersion:data:")]
		IntPtr Constructor (string formatIdentifier, string formatVersion, NSData data);

		[Export ("formatIdentifier", ArgumentSemantic.Copy)]
		string FormatIdentifier { get; }

		[Export ("formatVersion", ArgumentSemantic.Copy)]
		string FormatVersion { get; }

		[Export ("data", ArgumentSemantic.Strong)]
		NSData Data { get; }
	}

	[iOS (8,0)]
	[BaseType (typeof (PHObject))]
	interface PHAsset {

		[Export ("mediaType")]
		PHAssetMediaType MediaType { get; }

		[Export ("mediaSubtypes")]
		PHAssetMediaSubtype MediaSubtypes { get; }

		[Export ("pixelWidth")]
		nuint PixelWidth { get; }

		[Export ("pixelHeight")]
		nuint PixelHeight { get; }

		[Export ("creationDate", ArgumentSemantic.Strong)]
		NSDate CreationDate { get; }

		[Export ("modificationDate", ArgumentSemantic.Strong)]
		NSDate ModificationDate { get; }

		[Export ("location", ArgumentSemantic.Strong)]
		CLLocation Location { get; }

		[Export ("duration", ArgumentSemantic.Assign)]
		double Duration { get; }

		[Export ("hidden")]
		bool Hidden { [Bind ("isHidden")] get; }

		[Export ("favorite")]
		bool Favorite { [Bind ("isFavorite")] get; }

		[Export ("burstIdentifier", ArgumentSemantic.Strong)]
		string BurstIdentifier { get; }

		[Export ("burstSelectionTypes")]
		PHAssetBurstSelectionType BurstSelectionTypes { get; }

		[Export ("representsBurst")]
		bool RepresentsBurst { get; }

		[Export ("canPerformEditOperation:")]
		bool CanPerformEditOperation (PHAssetEditOperation editOperation);

		[Static]
		[Export ("fetchAssetsInAssetCollection:options:")]
		PHFetchResult FetchAssets (PHAssetCollection assetCollection, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchAssetsWithMediaType:options:")]
		PHFetchResult FetchAssets (PHAssetMediaType mediaType, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchAssetsWithLocalIdentifiers:options:")]
		PHFetchResult FetchAssetsUsingLocalIdentifiers (string[] identifiers, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchKeyAssetsInAssetCollection:options:")]
		PHFetchResult FetchKeyAssets (PHAssetCollection assetCollection, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchAssetsWithBurstIdentifier:options:")]
		PHFetchResult FetchAssets (string burstIdentifier, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchAssetsWithOptions:")]
		PHFetchResult FetchAssets ([NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchAssetsWithALAssetURLs:options:")]
		PHFetchResult FetchAssets (NSUrl[] assetUrls, [NullAllowed] PHFetchOptions options);

		[iOS (9,0)]
		[Export ("sourceType", ArgumentSemantic.Assign)]
		PHAssetSourceType SourceType { get; }
	}

	[iOS (8,0)]
	[DisableDefaultCtor] // Objective-C exception thrown.  Name: NSInvalidArgumentException Reason: -[PHAssetChangeRequest init]: unrecognized selector sent to instance 0x8165d150
	[BaseType (typeof (NSObject))]
	interface PHAssetChangeRequest {

		[Static]
		[Export ("creationRequestForAssetFromImage:")]
		PHAssetChangeRequest FromImage (UIImage image);

		[Static]
		[Export ("creationRequestForAssetFromImageAtFileURL:")]
		PHAssetChangeRequest FromImage (NSUrl fileUrl);

		[Static]
		[Export ("creationRequestForAssetFromVideoAtFileURL:")]
		PHAssetChangeRequest FromVideo (NSUrl fileUrl);

		[Export ("placeholderForCreatedAsset", ArgumentSemantic.Strong)]
		PHObjectPlaceholder PlaceholderForCreatedAsset { get; }

		[Static]
		[Export ("deleteAssets:")]
		void DeleteAssets (PHAsset[] assets);

		[Static]
		[Export ("changeRequestForAsset:")]
		PHAssetChangeRequest ChangeRequest (PHAsset asset);

		[Export ("creationDate", ArgumentSemantic.Strong)]
		NSDate CreationDate { get; set; }

		[Export ("location", ArgumentSemantic.Strong)]
		CLLocation Location { get; set; }

		[Export ("favorite", ArgumentSemantic.Assign)]
		bool Favorite { [Bind ("isFavorite")] get; set; }

		[Export ("hidden", ArgumentSemantic.Assign)]
		bool Hidden { [Bind ("isHidden")] get; set; }

		[NullAllowed] // by default this property is null
		[Export ("contentEditingOutput", ArgumentSemantic.Strong)]
		PHContentEditingOutput ContentEditingOutput { get; set; }

		[Export ("revertAssetContentToOriginal")]
		void RevertAssetContentToOriginal ();

	}

	[iOS (9,0)]
	[BaseType (typeof(PHAssetChangeRequest))]
	[DisableDefaultCtor]
	interface PHAssetCreationRequest
	{
		[Static]
		[Export ("creationRequestForAsset")]
		PHAssetCreationRequest CreationRequestForAsset ();

		// +(BOOL)supportsAssetResourceTypes:(NSArray<NSNumber * __nonnull> * __nonnull)types;
		[Static]
		[Internal, Export ("supportsAssetResourceTypes:")]
		bool _SupportsAssetResourceTypes (NSNumber[] types);

		[Export ("addResourceWithType:fileURL:options:")]
		void AddResource (PHAssetResourceType type, NSUrl fileURL, [NullAllowed] PHAssetResourceCreationOptions options);

		[Export ("addResourceWithType:data:options:")]
		void AddResource (PHAssetResourceType type, NSData data, [NullAllowed] PHAssetResourceCreationOptions options);
	}

	delegate void PHProgressHandler (double progress, ref bool stop);

	[iOS (9,0)]
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor] // crashes: -[PHAssetResource init]: unrecognized selector sent to instance 0x7f9e15884e90
	interface PHAssetResource
	{
		[Export ("type", ArgumentSemantic.Assign)]
		PHAssetResourceType ResourceType { get; }

		[Export ("assetLocalIdentifier")]
		string AssetLocalIdentifier { get; }

		[Export ("uniformTypeIdentifier")]
		string UniformTypeIdentifier { get; }

		[Export ("originalFilename")]
		string OriginalFilename { get; }

		[Static]
		[Export ("assetResourcesForAsset:")]
		PHAssetResource[] GetAssetResources (PHAsset forAsset);

		[iOS (9,1)]
		[Static]
		[Export ("assetResourcesForLivePhoto:")]
		PHAssetResource[] GetAssetResources (PHLivePhoto livePhoto);
	}

	[iOS (9,0)]
	[BaseType (typeof(NSObject))]
	interface PHAssetResourceCreationOptions : NSCopying
	{
		[NullAllowed, Export ("originalFilename")]
		string OriginalFilename { get; set; }

		[NullAllowed, Export ("uniformTypeIdentifier")]
		string UniformTypeIdentifier { get; set; }

		[Export ("shouldMoveFile")]
		bool ShouldMoveFile { get; set; }
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHContentEditingInputRequestOptions {

		[Export ("canHandleAdjustmentData", ArgumentSemantic.Copy)]
		Func<PHAdjustmentData, bool> CanHandleAdjustmentData { get; set; }

		[Export ("networkAccessAllowed", ArgumentSemantic.Assign)]
		bool NetworkAccessAllowed { [Bind ("isNetworkAccessAllowed")] get; set; }

		[NullAllowed, Export ("progressHandler", ArgumentSemantic.Copy)]
		PHProgressHandler ProgressHandler { get; set; }

		[Field ("PHContentEditingInputResultIsInCloudKey")]
		NSString ResultIsInCloudKey { get; }

		[Field ("PHContentEditingInputCancelledKey")]
		NSString CancelledKey { get; }

		[Field ("PHContentEditingInputErrorKey")]
		NSString InputErrorKey { get; }
	}

	delegate void PHContentEditingHandler (PHContentEditingInput contentEditingInput, NSDictionary requestStatusInfo);

	[iOS (8,0)]
	[Category]
	[BaseType (typeof (PHAsset))]
	interface PHAssetContentEditingInputExtensions {

		[Export ("requestContentEditingInputWithOptions:completionHandler:")]
		nuint RequestContentEditingInput ([NullAllowed] PHContentEditingInputRequestOptions options, PHContentEditingHandler completionHandler);

		[Export ("cancelContentEditingInputRequest:")]
		void CancelContentEditingInputRequest (nuint requestID);
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor] // fails when calling ToString (see below) and there are (static) API to create them
	// NSInternalInconsistencyException Reason: This method can only be called from inside of -[PHPhotoLibrary performChanges:] or -[PHPhotoLibrary performChangeAndWait:]
	interface PHAssetCollectionChangeRequest {

		[Static]
		[Export ("creationRequestForAssetCollectionWithTitle:")]
		PHAssetCollectionChangeRequest CreateAssetCollection (string title);

		[Export ("placeholderForCreatedAssetCollection", ArgumentSemantic.Strong)]
		PHObjectPlaceholder PlaceholderForCreatedAssetCollection { get; }

		[Static]
		[Export ("deleteAssetCollections:")]
		void DeleteAssetCollections (PHAssetCollection[] assetCollections);

		[Static]
		[Export ("changeRequestForAssetCollection:")]
		PHAssetCollectionChangeRequest ChangeRequest (PHAssetCollection assetCollection);

		[Static]
		[Export ("changeRequestForAssetCollection:assets:")]
		PHAssetCollectionChangeRequest ChangeRequest (PHAssetCollection assetCollection, PHFetchResult assets);

		[Export ("title", ArgumentSemantic.Strong)]
		string Title { get; set; }

		[Export ("addAssets:")]
		void AddAssets (PHObject [] assets);

		[Export ("insertAssets:atIndexes:")]
		void InsertAssets (PHObject [] assets, NSIndexSet indexes);

		[Export ("removeAssets:")]
		void RemoveAssets (PHObject[] assets);

		[Export ("removeAssetsAtIndexes:")]
		void RemoveAssets (NSIndexSet indexes);

		[Export ("replaceAssetsAtIndexes:withAssets:")]
		void ReplaceAssets (NSIndexSet indexes, PHObject[] assets);

		[Export ("moveAssetsAtIndexes:toIndex:")]
		void MoveAssets (NSIndexSet fromIndexes, nuint toIndex);
	}

	[iOS (9,0)]
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface PHAssetResourceManager
	{
		[Static]
		[Export ("defaultManager")]
		PHAssetResourceManager DefaultManager { get; }

		[Export ("requestDataForAssetResource:options:dataReceivedHandler:completionHandler:")]
		int RequestData (PHAssetResource forResource, [NullAllowed] PHAssetResourceRequestOptions options, Action<NSData> handler, Action<NSError> completionHandler);

		[Export ("writeDataForAssetResource:toFile:options:completionHandler:")]
		void WriteData (PHAssetResource forResource, NSUrl fileURL, [NullAllowed] PHAssetResourceRequestOptions options, Action<NSError> completionHandler);

		[Export ("cancelDataRequest:")]
		void CancelDataRequest (int requestID);
	}

	[iOS (9,0)]
	[BaseType (typeof(NSObject))]
	interface PHAssetResourceRequestOptions : NSCopying
	{
		[Export ("networkAccessAllowed")]
		bool NetworkAccessAllowed { [Bind ("isNetworkAccessAllowed")] get; set; }

		[NullAllowed, Export ("progressHandler", ArgumentSemantic.Copy)]
		Action<double> ProgressHandler { get; set; }
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHChange {

		[Export ("changeDetailsForObject:")]
		PHObjectChangeDetails GetObjectChangeDetails ([NullAllowed] PHObject obj);

		[Export ("changeDetailsForFetchResult:")]
		PHFetchResultChangeDetails GetFetchResultChangeDetails (PHFetchResult obj);
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHObjectChangeDetails {

		[Export ("objectBeforeChanges", ArgumentSemantic.Strong)]
		NSObject ObjectBeforeChanges { get; }

		[Export ("objectAfterChanges", ArgumentSemantic.Strong)]
		NSObject ObjectAfterChanges { get; }

		[Export ("assetContentChanged")]
		bool AssetContentChanged { get; }

		[Export ("objectWasDeleted")]
		bool ObjectWasDeleted { get; }
	}

	delegate void PHChangeDetailEnumerator (nuint fromIndex, nuint toIndex);

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHFetchResultChangeDetails {

		[Export ("fetchResultBeforeChanges", ArgumentSemantic.Strong)]
		PHFetchResult FetchResultBeforeChanges { get; }

		[Export ("fetchResultAfterChanges", ArgumentSemantic.Strong)]
		PHFetchResult FetchResultAfterChanges { get; }

		[Export ("hasIncrementalChanges", ArgumentSemantic.Assign)]
		bool HasIncrementalChanges { get; }

		[Export ("removedIndexes", ArgumentSemantic.Strong)]
		NSIndexSet RemovedIndexes { get; }

		[Export ("removedObjects", ArgumentSemantic.Strong)]
		PHObject[] RemovedObjects { get; }

		[Export ("insertedIndexes", ArgumentSemantic.Strong)]
		NSIndexSet InsertedIndexes { get; }

		[Export ("insertedObjects", ArgumentSemantic.Strong)]
		PHObject[] InsertedObjects { get; }

		[Export ("changedIndexes", ArgumentSemantic.Strong)]
		NSIndexSet ChangedIndexes { get; }

		[Export ("changedObjects", ArgumentSemantic.Strong)]
		PHObject[] ChangedObjects { get; }

		[Export ("enumerateMovesWithBlock:")]
		void EnumerateMoves (PHChangeDetailEnumerator handler);

		[Export ("hasMoves", ArgumentSemantic.Assign)]
		bool HasMoves { get; }

		[Static]
		[Export ("changeDetailsFromFetchResult:toFetchResult:changedObjects:")]
		PHFetchResultChangeDetails ChangeDetails (PHFetchResult fromResult, PHFetchResult toResult, PHObject[] changedObjects);
	}

	[iOS (8,0)]
	[BaseType (typeof (PHObject))]
	[DisableDefaultCtor] // not user createable (calling description fails, see below) must be fetched by API
	// NSInternalInconsistencyException Reason: PHCollection has no identifier
	interface PHCollection {

		[Export ("canContainAssets", ArgumentSemantic.Assign)]
		bool CanContainAssets { get; }

		[Export ("canContainCollections", ArgumentSemantic.Assign)]
		bool CanContainCollections { get; }

		[Export ("localizedTitle", ArgumentSemantic.Strong)]
		string LocalizedTitle { get; }

		[Export ("canPerformEditOperation:")]
		bool CanPerformEditOperation (PHCollectionEditOperation anOperation);

		[Static]
		[Export ("fetchCollectionsInCollectionList:options:")]
		PHFetchResult FetchCollections (PHCollectionList collectionList, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchTopLevelUserCollectionsWithOptions:")]
		PHFetchResult FetchTopLevelUserCollections ([NullAllowed] PHFetchOptions options);
	}

	[iOS (8,0)]
	[BaseType (typeof (PHCollection))]
	interface PHAssetCollection {

		[Export ("assetCollectionType")]
		PHAssetCollectionType AssetCollectionType { get; }

		[Export ("assetCollectionSubtype")]
		PHAssetCollectionSubtype AssetCollectionSubtype { get; }

		[Export ("estimatedAssetCount")]
		nuint EstimatedAssetCount { get; }

		[Export ("startDate", ArgumentSemantic.Strong)]
		NSDate StartDate { get; }

		[Export ("endDate", ArgumentSemantic.Strong)]
		NSDate EndDate { get; }

		[Export ("approximateLocation", ArgumentSemantic.Strong)]
		CLLocation ApproximateLocation { get; }

		[Export ("localizedLocationNames", ArgumentSemantic.Strong)]
		string[] LocalizedLocationNames { get; }

		[Static]
		[Export ("fetchAssetCollectionsWithLocalIdentifiers:options:")]
		PHFetchResult FetchAssetCollections (string[] identifiers, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchAssetCollectionsWithType:subtype:options:")]
		PHFetchResult FetchAssetCollections (PHAssetCollectionType type, PHAssetCollectionSubtype subtype, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchAssetCollectionsContainingAsset:withType:options:")]
		PHFetchResult FetchAssetCollections (PHAsset asset, PHAssetCollectionType type, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchAssetCollectionsWithALAssetGroupURLs:options:")]
		PHFetchResult FetchAssetCollections (NSUrl[] assetGroupUrls, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchMomentsInMomentList:options:")]
		PHFetchResult FetchMoments (PHCollectionList momentList, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchMomentsWithOptions:")]
		PHFetchResult FetchMoments ([NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("transientAssetCollectionWithAssets:title:")]
		PHAssetCollection GetTransientAssetCollection (PHAsset[] assets, string title);

		[Static]
		[Export ("transientAssetCollectionWithAssetFetchResult:title:")]
		PHAssetCollection GetTransientAssetCollection (PHFetchResult fetchResult, string title);
	}

	[iOS (8,0)]
	[BaseType (typeof (PHCollection))]
	interface PHCollectionList {

		[Export ("collectionListType")]
		PHCollectionListType CollectionListType { get; }

		[Export ("collectionListSubtype")]
		PHCollectionListSubtype CollectionListSubtype { get; }

		[Export ("startDate", ArgumentSemantic.Strong)]
		NSDate StartDate { get; }

		[Export ("endDate", ArgumentSemantic.Strong)]
		NSDate EndDate { get; }

		[Export ("localizedLocationNames", ArgumentSemantic.Strong)]
		string[] LocalizedLocationNames { get; }

		[Static]
		[Export ("fetchCollectionListsContainingCollection:options:")]
		PHFetchResult FetchCollectionLists (PHCollection collection, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchCollectionListsWithLocalIdentifiers:options:")]
		PHFetchResult FetchCollectionLists (string[] identifiers, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchCollectionListsWithType:subtype:options:")]
		PHFetchResult FetchCollectionLists (PHCollectionListType type, PHCollectionListSubtype subType, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchMomentListsWithSubtype:containingMoment:options:")]
		PHFetchResult FetchMomentLists (PHCollectionListSubtype subType, PHAssetCollection moment, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("fetchMomentListsWithSubtype:options:")]
		PHFetchResult FetchMomentLists (PHCollectionListSubtype subType, [NullAllowed] PHFetchOptions options);

		[Static]
		[Export ("transientCollectionListWithCollections:title:")]
		PHCollectionList CreateTransientCollectionList (PHAssetCollection[] collections, string title);

		[Static]
		[Export ("transientCollectionListWithCollectionsFetchResult:title:")]
		PHCollectionList CreateTransientCollectionList (PHFetchResult fetchResult, string title);
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor] // sometimes crash when calling 'description'
	// This method can only be called from inside of -[PHPhotoLibrary performChanges:] or -[PHPhotoLibrary performChangeAndWait:]
	// as it ties to get 'title' which was never set (e.g. using FromCreationRequest)
	interface PHCollectionListChangeRequest {

		[Static]
		[Export ("creationRequestForCollectionListWithTitle:")]
		PHCollectionListChangeRequest CreateAssetCollection (string title);

		[Export ("placeholderForCreatedCollectionList", ArgumentSemantic.Strong)]
		PHObjectPlaceholder PlaceholderForCreatedCollectionList { get; }

		[Static]
		[Export ("deleteCollectionLists:")]
		void DeleteCollectionLists (PHCollectionList[] collectionLists);

		[Static]
		[Export ("changeRequestForCollectionList:")]
		PHCollectionListChangeRequest ChangeRequest (PHCollectionList collectionList);

		[Static]
		[Export ("changeRequestForCollectionList:childCollections:")]
		PHCollectionListChangeRequest ChangeRequest (PHCollectionList collectionList, PHFetchResult childCollections);

		[Export ("title", ArgumentSemantic.Strong)]
		string Title { get; set; }

		[Export ("addChildCollections:")]
		void AddChildCollections (PHCollection[] collections);

		[Export ("insertChildCollections:atIndexes:")]
		void InsertChildCollections (PHCollection[] collections, NSIndexSet indexes);

		[Export ("removeChildCollections:")]
		void RemoveChildCollections (PHCollection[] collections);

		[Export ("removeChildCollectionsAtIndexes:")]
		void RemoveChildCollections (NSIndexSet indexes);

		[Export ("replaceChildCollectionsAtIndexes:withChildCollections:")]
		void ReplaceChildCollection (NSIndexSet indexes, PHCollection[] collections);

		[Export ("moveChildCollectionsAtIndexes:toIndex:")]
		void MoveChildCollections (NSIndexSet indexes, nuint toIndex);
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHContentEditingInput {

		[Export ("mediaType")]
		PHAssetMediaType MediaType { get; }

		[Export ("mediaSubtypes")]
		PHAssetMediaSubtype MediaSubtypes { get; }

		[Export ("creationDate", ArgumentSemantic.Copy)]
		NSDate CreationDate { get; }

		[Export ("location", ArgumentSemantic.Copy)]
		CLLocation Location { get; }

		[Export ("uniformTypeIdentifier")]
		string UniformTypeIdentifier { get; }

		[Export ("adjustmentData", ArgumentSemantic.Strong)]
		PHAdjustmentData AdjustmentData { get; }

		[Export ("displaySizeImage", ArgumentSemantic.Strong)]
		UIImage DisplaySizeImage { get; }

		[Export ("fullSizeImageURL", ArgumentSemantic.Copy)]
		NSUrl FullSizeImageUrl { get; }

		[Export ("fullSizeImageOrientation")]
		XamCore.CoreImage.CIImageOrientation FullSizeImageOrientation { get; }

		[Export ("avAsset", ArgumentSemantic.Strong)]
		[Availability (Deprecated = Platform.iOS_9_0, Message="In iOS 9, use AudiovisualAsset property instead")]
		AVAsset AvAsset { get; }

		[iOS (9,0)]
		[NullAllowed, Export ("audiovisualAsset", ArgumentSemantic.Strong)]
		AVAsset AudiovisualAsset { get; }
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHContentEditingOutput : NSCoding, NSSecureCoding {

		[Export ("initWithContentEditingInput:")]
		IntPtr Constructor (PHContentEditingInput contentEditingInput);

		[Export ("initWithPlaceholderForCreatedAsset:")]
		IntPtr Constructor (PHObjectPlaceholder placeholderForCreatedAsset);

		[NullAllowed] // by default this property is null
		[Export ("adjustmentData", ArgumentSemantic.Strong)]
		PHAdjustmentData AdjustmentData { get; set; }

		[Export ("renderedContentURL", ArgumentSemantic.Copy)]
		NSUrl RenderedContentUrl { get; }
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHFetchOptions : NSCopying {

		[NullAllowed] // by default this property is null
		[Export ("predicate", ArgumentSemantic.Strong)]
		NSPredicate Predicate { get; set; }

		[NullAllowed] // by default this property is null
		[Export ("sortDescriptors", ArgumentSemantic.Strong)]
		NSSortDescriptor[] SortDescriptors { get; set; }

		[Export ("includeHiddenAssets")]
		bool IncludeHiddenAssets { get; set; }

		[Export ("includeAllBurstAssets", ArgumentSemantic.Assign)]
		bool IncludeAllBurstAssets { get; set; }

		[Export ("wantsIncrementalChangeDetails", ArgumentSemantic.Assign)]
		bool WantsIncrementalChangeDetails { get; set; }

		[iOS (9,0)]
		[Export ("includeAssetSourceTypes", ArgumentSemantic.Assign)]
		PHAssetSourceType IncludeAssetSourceTypes { get; set; }

		[iOS (9,0)]
		[Export ("fetchLimit", ArgumentSemantic.Assign)]
		nuint FetchLimit { get; set; }
	}

	delegate void PHFetchResultEnumerator (NSObject element, nuint elementIndex, out bool stop);

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor] // crash when calling 'description' and seems to be only returned from iOS (not user created)
	interface PHFetchResult : NSCopying {

		[Export ("count")]
		nint Count { get; }

		[Export ("objectAtIndex:")]
		NSObject ObjectAt (nint index);

		[Internal, Export ("objectAtIndexedSubscript:")]
		NSObject _ObjectAtIndexedSubscript (nint index);

		[Export ("containsObject:")]
		bool Contains (NSObject id);

		[Export ("indexOfObject:")]
		nint IndexOf (NSObject id);

		[Export ("indexOfObject:inRange:")]
		nint IndexOf (NSObject id, NSRange range);

		[Export ("firstObject")]
		NSObject firstObject { get; }

		[Export ("lastObject")]
		NSObject LastObject { get; }

		[Internal, Export ("objectsAtIndexes:")]
		IntPtr _ObjectsAt (NSIndexSet indexes);

		[Export ("enumerateObjectsUsingBlock:")]
		void Enumerate (PHFetchResultEnumerator handler);

		[Export ("enumerateObjectsWithOptions:usingBlock:")]
		void Enumerate (NSEnumerationOptions opts, PHFetchResultEnumerator handler);

		[Export ("enumerateObjectsAtIndexes:options:usingBlock:")]
		void Enumerate (NSIndexSet idx, NSEnumerationOptions opts, PHFetchResultEnumerator handler);

		[Export ("countOfAssetsWithMediaType:")]
		nuint CountOfAssetsWithMediaType (PHAssetMediaType mediaType);
	}

	delegate void PHAssetImageProgressHandler (double progress, NSError error, out bool stop, NSDictionary info);

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHImageRequestOptions : NSCopying {

		[Export ("version", ArgumentSemantic.Assign)]
		PHImageRequestOptionsVersion Version { get; set; }

		[Export ("deliveryMode", ArgumentSemantic.Assign)]
		PHImageRequestOptionsDeliveryMode DeliveryMode { get; set; }

		[Export ("resizeMode", ArgumentSemantic.Assign)]
		PHImageRequestOptionsResizeMode ResizeMode { get; set; }

		[Export ("normalizedCropRect", ArgumentSemantic.Assign)]
		CGRect NormalizedCropRect { get; set; }

		[Export ("networkAccessAllowed", ArgumentSemantic.Assign)]
		bool NetworkAccessAllowed { [Bind ("isNetworkAccessAllowed")] get; set; }

		[Export ("synchronous", ArgumentSemantic.Assign)]
		bool Synchronous { [Bind ("isSynchronous")] get; set; }

		[Export ("progressHandler", ArgumentSemantic.Copy)] [NullAllowed]
		PHAssetImageProgressHandler ProgressHandler { get; set; }
	}

	delegate void PHAssetVideoProgressHandler (double progress, NSError error, out bool stop, NSDictionary info);

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHVideoRequestOptions {

		[Export ("networkAccessAllowed", ArgumentSemantic.Assign)]
		bool NetworkAccessAllowed { [Bind ("isNetworkAccessAllowed")] get; set; }

		[Export ("version", ArgumentSemantic.Assign)]
		PHVideoRequestOptionsVersion Version { get; set; }

		[Export ("deliveryMode", ArgumentSemantic.Assign)]
		PHVideoRequestOptionsDeliveryMode DeliveryMode { get; set; }

		[NullAllowed] // by default this property is null
		[Export ("progressHandler", ArgumentSemantic.Copy)]
		PHAssetVideoProgressHandler ProgressHandler { get; set; }
	}

	[iOS (8,0)]
	[Static]
	interface PHImageKeys {

		[Field ("PHImageResultIsInCloudKey")]
		NSString ResultIsInCloud { get; }

		[Field ("PHImageResultIsDegradedKey")]
		NSString ResultIsDegraded { get; }

		[Field ("PHImageCancelledKey")]
		NSString Cancelled { get; }

		[Field ("PHImageErrorKey")]
		NSString Error { get; }

		[Field ("PHImageResultRequestIDKey")]
		NSString ResultRequestID { get; }
	}

	delegate void PHImageResultHandler (UIImage result, NSDictionary info);
	delegate void PHImageManagerRequestPlayerHandler (AVPlayerItem playerItem, NSDictionary info);
	delegate void PHImageManagerRequestExportHandler (AVAssetExportSession exportSession, NSDictionary info);
#if XAMCORE_4_0
	delegate void PHImageManagerRequestAVAssetHandler (AVAsset asset, AVAudioMix audioMix, NSDictionary info);
#else
	delegate void PHImageManagerRequestAvAssetHandler (AVAsset asset, AVAudioMix audioMix, NSDictionary info);
#endif
	delegate void PHImageManagerRequestLivePhoto (PHLivePhoto livePhoto, NSDictionary info);

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	interface PHImageManager {

		[Static]
		[Export ("defaultManager")]
		PHImageManager DefaultManager { get; }

		[Export ("requestImageForAsset:targetSize:contentMode:options:resultHandler:")]
		int /* PHImageRequestID = int32_t */ RequestImageForAsset (PHAsset asset, CGSize targetSize, PHImageContentMode contentMode, [NullAllowed] PHImageRequestOptions options, PHImageResultHandler resultHandler);

		[Export ("cancelImageRequest:")]
		void CancelImageRequest (int /* PHImageRequestID = int32_t */ requestID);

		[Export ("requestImageDataForAsset:options:resultHandler:")]
		int /* PHImageRequestID = int32_t */ RequestImageData (PHAsset asset, [NullAllowed] PHImageRequestOptions options, PHImageDataHandler handler);

		[Export ("requestPlayerItemForVideo:options:resultHandler:")]
		int /* PHImageRequestID = int32_t */ RequestPlayerItem (PHAsset asset, [NullAllowed] PHVideoRequestOptions options, PHImageManagerRequestPlayerHandler resultHandler);

		[Export ("requestExportSessionForVideo:options:exportPreset:resultHandler:")]
		int /* PHImageRequestID = int32_t */ RequestExportSession (PHAsset asset, [NullAllowed] PHVideoRequestOptions options, string exportPreset, PHImageManagerRequestExportHandler resultHandler);

		[Export ("requestAVAssetForVideo:options:resultHandler:")]
#if XAMCORE_4_0
		int /* PHImageRequestID = int32_t */ RequestAVAsset (PHAsset asset, [NullAllowed] PHVideoRequestOptions options, PHImageManagerRequestAVAssetHandler resultHandler);
#else
		int /* PHImageRequestID = int32_t */ RequestAvAsset (PHAsset asset, [NullAllowed] PHVideoRequestOptions options, PHImageManagerRequestAvAssetHandler resultHandler);
#endif

		[Field ("PHImageManagerMaximumSize")]
		CGSize MaximumSize { get; }

		[iOS (9,1)]
		[Export ("requestLivePhotoForAsset:targetSize:contentMode:options:resultHandler:")]
		int /* PHImageRequestID = int32_t */ RequestLivePhoto (PHAsset asset, CGSize targetSize, PHImageContentMode contentMode, [NullAllowed] PHLivePhotoRequestOptions options, PHImageManagerRequestLivePhoto resultHandler);
	}

	public delegate void PHImageDataHandler (NSData data, NSString dataUti, UIImageOrientation orientation, NSDictionary info);

	[iOS (8,0)]
	[BaseType (typeof (PHImageManager))]
	interface PHCachingImageManager {

		[Export ("allowsCachingHighQualityImages", ArgumentSemantic.Assign)]
		bool AllowsCachingHighQualityImages { get; set; }

		[Export ("startCachingImagesForAssets:targetSize:contentMode:options:")]
		void StartCaching (PHAsset [] assets, CGSize targetSize, PHImageContentMode contentMode, [NullAllowed] PHImageRequestOptions options);

		[Export ("stopCachingImagesForAssets:targetSize:contentMode:options:")]
		void StopCaching (PHAsset [] assets, CGSize targetSize, PHImageContentMode contentMode, [NullAllowed] PHImageRequestOptions options);

		[Export ("stopCachingImagesForAllAssets")]
		void StopCaching ();
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor] // doc -> "abstract base class"
	// throws "NSInternalInconsistencyException Reason: PHObject has no identifier"
	interface PHObject : NSCopying {

		[Export ("localIdentifier", ArgumentSemantic.Copy)]
		string LocalIdentifier { get; }
	}

	[iOS (8,0)]
	[BaseType (typeof (PHObject))]
	interface PHObjectPlaceholder {

	}

	[iOS (8,0)]
	[Protocol]
	[Model]
	[BaseType (typeof (NSObject))]
	interface PHPhotoLibraryChangeObserver {

#if XAMCORE_2_0
		[Abstract]
#endif
		[Export ("photoLibraryDidChange:")]
		void PhotoLibraryDidChange (PHChange changeInstance);
	}

	[iOS (8,0)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor] // NSInternalInconsistencyException Reason: -[PHPhotoLibrary init] unsupported
	interface PHPhotoLibrary {

		[Static]
		[Export ("sharedPhotoLibrary")]
		PHPhotoLibrary SharedPhotoLibrary { get; }

		[Static, Export ("authorizationStatus")]
		PHAuthorizationStatus AuthorizationStatus { get; }

		[Static, Export ("requestAuthorization:")]
		void RequestAuthorization (Action<PHAuthorizationStatus> handler);

		[Export ("performChanges:completionHandler:")]
		void PerformChanges (Action changeHandler, Action<bool, NSError> completionHandler);

		[Export ("performChangesAndWait:error:")]
		bool PerformChangesAndWait (Action changeHandler, out NSError error);

		[Export ("registerChangeObserver:")]
		void RegisterChangeObserver ([Protocolize] PHPhotoLibraryChangeObserver observer);

		[Export ("unregisterChangeObserver:")]
		void UnregisterChangeObserver ([Protocolize] PHPhotoLibraryChangeObserver observer);
	}

	[iOS (9,1)]
	[BaseType (typeof(NSObject))]
	public interface PHLivePhoto : NSSecureCoding, NSCopying
	{
		[Export ("size")]
		CGSize Size { get; }

		[Static]
		[Export ("requestLivePhotoWithResourceFileURLs:placeholderImage:targetSize:contentMode:resultHandler:")]
		int RequestLivePhoto (NSUrl[] fileUrls, [NullAllowed] UIImage image, CGSize targetSize, PHImageContentMode contentMode, Action<PHLivePhoto, NSDictionary> resultHandler);

		[Static]
		[Export ("cancelLivePhotoRequestWithRequestID:")]
		void CancelLivePhotoRequest (int requestID);
	}

	[iOS (9,1)]
	[BaseType (typeof (NSObject))]
	interface PHLivePhotoRequestOptions : NSCopying	{
		[Export ("deliveryMode", ArgumentSemantic.Assign)]
		PHImageRequestOptionsDeliveryMode DeliveryMode { get; set; }

		[Export ("networkAccessAllowed")]
		bool NetworkAccessAllowed { [Bind ("isNetworkAccessAllowed")] get; set; }

		[NullAllowed, Export ("progressHandler", ArgumentSemantic.Copy)]
		PHAssetImageProgressHandler ProgressHandler { get; set; }
	}

	[iOS (9,1)]
	[Static]
	interface PHLivePhotoInfo {
		[Field ("PHLivePhotoInfoErrorKey")]
		NSString ErrorKey { get; }

		[Field ("PHLivePhotoInfoIsDegradedKey")]
		NSString IsDegradedKey { get; }

		[Field ("PHLivePhotoInfoCancelledKey")]
		NSString CancelledKey { get; }
	}
}
