// OpenCVApplication.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "common.h"
#include <random>
#include<vector>
#include <queue>
#include<cmath>
#include<fstream>
#include <cstdlib>
#include<iostream>

using namespace std;
using namespace cv;

void testOpenImage()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat src;
		src = imread(fname);
		imshow("image", src);
		waitKey();
	}
}

void testOpenImagesFld()
{
	char folderName[MAX_PATH];
	if (openFolderDlg(folderName) == 0)
		return;
	char fname[MAX_PATH];
	FileGetter fg(folderName, "bmp");
	while (fg.getNextAbsFile(fname))
	{
		Mat src;
		src = imread(fname);
		imshow(fg.getFoundFileName(), src);
		if (waitKey() == 27) //ESC pressed
			break;
	}
}

void testResize()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat src;
		src = imread(fname);
		Mat dst1, dst2;
		//without interpolation
		resizeImg(src, dst1, 320, false);
		//with interpolation
		resizeImg(src, dst2, 320, true);
		imshow("input image", src);
		imshow("resized image (without interpolation)", dst1);
		imshow("resized image (with interpolation)", dst2);
		waitKey();
	}
}

void testVideoSequence()
{
	cv::VideoCapture cap("Videos/rubic.avi"); // off-line video from file
											  //VideoCapture cap(0);	// live video from web cam
	if (!cap.isOpened()) {
		printf("Cannot open video capture device.\n");
		waitKey(0);
		return;
	}

	Mat edges;
	Mat frame;
	char c;

	while (cap.read(frame))
	{
		Mat grayFrame;
		cvtColor(frame, grayFrame, CV_BGR2GRAY);
		imshow("source", frame);
		imshow("gray", grayFrame);
		c = cvWaitKey(0);  // waits a key press to advance to the next frame
		if (c == 27) {
			// press ESC to exit
			printf("ESC pressed - capture finished\n");
			break;  //ESC pressed
		};
	}
}

void testSnap()
{
	cv::VideoCapture cap(0); // open the deafult camera (i.e. the built in web cam)
	if (!cap.isOpened()) // openenig the video device failed
	{
		printf("Cannot open video capture device.\n");
		return;
	}

	Mat frame;
	char numberStr[256];
	char fileName[256];

	// video resolution
	Size capS = Size((int)cap.get(CV_CAP_PROP_FRAME_WIDTH),
		(int)cap.get(CV_CAP_PROP_FRAME_HEIGHT));

	// Display window
	const char* WIN_SRC = "Src"; //window for the source frame
	namedWindow(WIN_SRC, CV_WINDOW_AUTOSIZE);
	cvMoveWindow(WIN_SRC, 0, 0);

	const char* WIN_DST = "Snapped"; //window for showing the snapped frame
	namedWindow(WIN_DST, CV_WINDOW_AUTOSIZE);
	cvMoveWindow(WIN_DST, capS.width + 10, 0);

	char c;
	int frameNum = -1;
	int frameCount = 0;

	for (;;)
	{
		cap >> frame; // get a new frame from camera
		if (frame.empty())
		{
			printf("End of the video file\n");
			break;
		}

		++frameNum;

		imshow(WIN_SRC, frame);

		c = cvWaitKey(10);  // waits a key press to advance to the next frame
		if (c == 27) {
			// press ESC to exit
			printf("ESC pressed - capture finished");
			break;  //ESC pressed
		}
		if (c == 115) { //'s' pressed - snapp the image to a file
			frameCount++;
			fileName[0] = NULL;
			sprintf(numberStr, "%d", frameCount);
			strcat(fileName, "Images/A");
			strcat(fileName, numberStr);
			strcat(fileName, ".bmp");
			bool bSuccess = imwrite(fileName, frame);
			if (!bSuccess)
			{
				printf("Error writing the snapped image\n");
			}
			else
				imshow(WIN_DST, frame);
		}
	}

}

void MyCallBackFunc(int event, int x, int y, int flags, void* param)
{
	//More examples: http://opencvexamples.blogspot.com/2014/01/detect-mouse-clicks-and-moves-on-image.html
	Mat* src = (Mat*)param;
	if (event == CV_EVENT_LBUTTONDOWN)
	{
		printf("Pos(x,y): %d,%d  Color(RGB): %d,%d,%d\n",
			x, y,
			(int)(*src).at<Vec3b>(y, x)[2],
			(int)(*src).at<Vec3b>(y, x)[1],
			(int)(*src).at<Vec3b>(y, x)[0]);
	}
}

void testMouseClick()
{
	Mat src;
	// Read image from file 
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		src = imread(fname);
		//Create a window
		namedWindow("My Window", 1);

		//set the callback function for any mouse event
		setMouseCallback("My Window", MyCallBackFunc, &src);

		//show the image
		imshow("My Window", src);

		// Wait until user press some key
		waitKey(0);
	}
}

/* Histogram display function - display a histogram using bars (simlilar to L3 / PI)
Input:
name - destination (output) window name
hist - pointer to the vector containing the histogram values
hist_cols - no. of bins (elements) in the histogram = histogram image width
hist_height - height of the histogram image
Call example:
showHistogram ("MyHist", hist_dir, 255, 200);
*/
void showHistogram(const std::string& name, int* hist, const int  hist_cols, const int hist_height)
{
	Mat imgHist(hist_height, hist_cols, CV_8UC3, CV_RGB(255, 255, 255)); // constructs a white image

																		 //computes histogram maximum
	int max_hist = 0;
	for (int i = 0; i < hist_cols; i++)
		if (hist[i] > max_hist)
			max_hist = hist[i];
	double scale = 1.0;
	scale = (double)hist_height / max_hist;
	int baseline = hist_height - 1;

	for (int x = 0; x < hist_cols; x++) {
		Point p1 = Point(x, baseline);
		Point p2 = Point(x, baseline - cvRound(hist[x] * scale));
		line(imgHist, p1, p2, CV_RGB(255, 0, 255)); // histogram bins colored in magenta
	}

	imshow(name, imgHist);
}

void negative_image()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
				img.at<uchar>(i, j) = 255 - img.at<uchar>(i, j);

		imshow("negative image", img);
		strcat(fname, "_negative.bmp");
		imwrite(fname, img);
		waitKey(0);
	}
}

void modify_add(int factor)
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
			{
				if (img.at<uchar>(i, j) + factor > 255)
					img.at<uchar>(i, j) = 255;
				else if (img.at<uchar>(i, j) + factor < 0)
					img.at<uchar>(i, j) = 0;
				else
					img.at<uchar>(i, j) = img.at<uchar>(i, j) + factor;
			}
		imshow("additive image", img);
		strcat(fname, "_additive.bmp");
		imwrite(fname, img);
		waitKey(0);
	}
}

void modify_mul(float factor)
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname,
			CV_LOAD_IMAGE_GRAYSCALE);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
			{
				if (img.at<uchar>(i, j) * factor > 255)
					img.at<uchar>(i, j) = 255;
				else
					img.at<uchar>(i, j) = img.at<uchar>(i, j) * factor;

			}
		imshow("additive image", img);
		strcat(fname, "_multiplicative.bmp");
		imwrite(fname, img);
		waitKey(0);
	}
}

void draw_square()
{
	Mat square(256, 256, CV_8UC3);
	for (int i = 0; i < square.rows; i++)
		for (int j = 0; j < square.cols; j++)
		{
			if (i < square.rows / 2)
			{
				if (j < square.cols / 2)
				{
					square.at<Vec3b>(i, j)[2] = 255;
					square.at<Vec3b>(i, j)[1] = 255;
					square.at<Vec3b>(i, j)[0] = 255;
				}
				else
				{
					square.at<Vec3b>(i, j)[2] = 0;
					square.at<Vec3b>(i, j)[1] = 255;
					square.at<Vec3b>(i, j)[0] = 0;
				}
			}
			else
			{
				if (j < square.cols / 2)
				{
					square.at<Vec3b>(i, j)[2] = 255;
					square.at<Vec3b>(i, j)[1] = 0;
					square.at<Vec3b>(i, j)[0] = 0;
				}
				else
				{
					square.at<Vec3b>(i, j)[2] = 255;
					square.at<Vec3b>(i, j)[1] = 255;
					square.at<Vec3b>(i, j)[0] = 0;
				}
			}
		}
	imshow("Square", square);
	imwrite("D:\\Andrei\\Scoala\\IP\\Proiect\\OpenCVApplication-VS2013_2413_basic\\Images\\square.bmp", square);
	waitKey(0);
}

void h_flip()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		for (int i = 0; i < img.rows / 2; i++)
			for (int j = 0; j < img.cols; j++)
			{
				uchar pixel = img.at<uchar>(i, j);
				img.at<uchar>(i, j) = img.at<uchar>(img.rows - 1 - i, j);
				img.at<uchar>(img.rows - 1 - i, j) = pixel;
			}
		imshow("horizontal flipped image", img);
		strcat(fname, "_hflip.bmp");
		imwrite(fname, img);
		waitKey(0);
	}
}

void v_flip()
{

	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname,
			CV_LOAD_IMAGE_COLOR);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols / 2; j++)
			{
				Vec3b pixel = img.at<Vec3b>(i, j);
				img.at<Vec3b>(i, j) = img.at<Vec3b>(i, img.cols - j - 1);
				img.at<Vec3b>(i, img.cols - j - 1) = pixel;
			}
		imshow("vertical flipped image", img);
		strcat(fname, "_vflip.bmp");
		imwrite(fname, img);
		waitKey(0);
	}
}

void center_crop(int w, int h)
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_COLOR);
		if (w > img.cols || h > img.rows)
			printf(" Given width and height are not good. please retry");
		else
		{
			Mat cropped(h, w, CV_8UC3);
			int ir, r = (img.rows - h) / 2;
			int ic, c = (img.cols - w) / 2;
			ir = r; ic = c;
			printf("%d %d %d %d %d %d \n", img.rows, img.cols, ir, r, ic, c);
			for (r = 0; r < h; r++)
				for (c = 0; c < w; c++)
					cropped.at<Vec3b>(r, c) = img.at<Vec3b>(r + ir, c + ic);
			imshow("cropped image", cropped);
			strcat(fname, "_ccrop.bmp");
			imwrite(fname, cropped);
			waitKey(0);
		}
	}
}

void separate_colors()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_COLOR);
		Mat red(img.rows, img.cols, CV_8UC1);
		Mat green(img.rows, img.cols, CV_8UC1);
		Mat blue(img.rows, img.cols, CV_8UC1);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
			{
				red.at<uchar>(i, j) = img.at<Vec3b>(i, j)[0];
				green.at<uchar>(i, j) = img.at<Vec3b>(i, j)[1];
				blue.at<uchar>(i, j) = img.at<Vec3b>(i, j)[2];
			}
		imshow("red", red);
		imshow("green", green);
		imshow("blue", blue);
	}
}

void grayscale()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_COLOR);
		Mat gray(img.rows, img.cols, CV_8UC1);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
			{
				gray.at<uchar>(i, j) = (img.at<Vec3b>(i, j)[0] + img.at<Vec3b>(i, j)[1] + img.at<Vec3b>(i, j)[2]) / 3;
			}
		imshow("gray", gray);
	}
}

void Threshold(int th)
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		Mat blackAndWhite(img.rows, img.cols, CV_8UC1);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
			{
				if (img.at<uchar>(i, j) > th)
					blackAndWhite.at<uchar>(i, j) = 255;
				else
					blackAndWhite.at<uchar>(i, j) = 0;
			}
		imshow("Black and white", blackAndWhite);
		waitKey(0);
	}
}

void convertToHSV()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_COLOR);
		Mat hue(img.rows, img.cols, CV_8UC1);
		Mat saturation(img.rows, img.cols, CV_8UC1);
		Mat value(img.rows, img.cols, CV_8UC1);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
			{
				float r = img.at<Vec3b>(i, j)[2] / 255.;
				float g = img.at<Vec3b>(i, j)[1] / 255.;
				float b = img.at<Vec3b>(i, j)[0] / 255.;
				float M = max(max(r, g), b);
				float m = min(min(r, g), b);
				float C = M - m;
				float V = M;
				float S;
				if (V)
					S = C / V;
				else
					S = 0;
				float H;
				if (C != 0.0)
				{
					if (M == r) H = 60 * (g - b) / C;
					if (M == g) H = 120 + 60 * (b - r) / C;
					if (M == b) H = 240 + 60 * (r - g) / C;
				}
				else // grayscale
					H = 0;
				if (H < 0.0)
					H = H + 360;
				hue.at<uchar>(i, j) = H * 255 / 360;
				saturation.at<uchar>(i, j) = S * 255;
				value.at<uchar>(i, j) = V * 255;
			}
		imshow("hue", hue);
		imshow("saturation", saturation);
		imshow("value", value);
		waitKey(0);
	}
}

void detectSign()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat BGRimg = imread(fname, CV_LOAD_IMAGE_COLOR);
		Mat HSVimg;
		cv::cvtColor(BGRimg, HSVimg, CV_BGR2HSV);

		imshow("img", HSVimg);
		Mat hsv_channels[3];
		cv::split(HSVimg, hsv_channels);
		Mat mask;
		int th_red_low = 0;
		int th_red_high = 15;
		cv::inRange(hsv_channels[0], th_red_low, th_red_high, mask);

		imshow("img", mask);
		waitKey(0);
	}
}


void onMouse(int event, int x, int y, int flags, void* param)
{
	//More examples: http://opencvexamples.blogspot.com/2014/01/detect-mouse-clicks-and-moves-on-image.html
	Mat* src = (Mat*)param;
	Mat mark((*src).rows, (*src).cols, CV_8UC3);
	if (event == CV_EVENT_LBUTTONDOWN)
	{
		int area = 0, CenterOfMassX = 0, CenterOfMassY = 0, cmin = 1000000, cmax = 0, rmin = 100000, rmax = 0;
		double sus = 0, jos = 0, perimeter = 0;
		Vec3b color = (*src).at<Vec3b>(y, x);
		Vec3b white = Vec3b(255, 255, 255);
		for (int i = 1; i < (*src).rows - 1; i++)
			for (int j = 1; j < (*src).cols - 1; j++)
			{
				mark.at<Vec3b>(i, j) = (*src).at<Vec3b>(i, j);
				if ((*src).at<Vec3b>(i, j) == color)
				{
					area++;
					if ((*src).at<Vec3b>(i - 1, j) == white ||
						(*src).at<Vec3b>(i - 1, j - 1) == white ||
						(*src).at<Vec3b>(i, j - 1) == white ||
						(*src).at<Vec3b>(i + 1, j - 1) == white ||
						(*src).at<Vec3b>(i + 1, j) == white ||
						(*src).at<Vec3b>(i + 1, j + 1) == white ||
						(*src).at<Vec3b>(i, j + 1) == white ||
						(*src).at<Vec3b>(i - 1, j + 1) == white)
					{
						perimeter++;
						mark.at<Vec3b>(i, j) = Vec3b(0, 0, 255);
					}
					CenterOfMassX += i;
					CenterOfMassY += j;
				}
			}
		CenterOfMassX /= area;
		CenterOfMassY /= area;
		perimeter *= PI / 4;
		double T = 4 * PI *(area / (perimeter*perimeter));
		for (int i = 0; i < (*src).rows; i++)
			for (int j = 0; j < (*src).cols; j++)
				if ((*src).at<Vec3b>(i, j) == color)
				{
					sus += (i - CenterOfMassX)*(j - CenterOfMassY);
					jos += (j - CenterOfMassY)*(j - CenterOfMassY);
					jos -= (i - CenterOfMassX)*(i - CenterOfMassX);
					if (i < rmin)
						rmin = i;
					if (i > rmax)
						rmax = i;
					if (j < cmin)
						cmin = j;
					if (j > cmax)
						cmax = j;
				}
		double axisOfElongation = (atan2(2 * sus, jos) * 180 / PI) / 2.;
		double R = ((double)cmax - (double)cmin + 1) / ((double)rmax - (double)rmin + 1);
		printf(" Area: %d, Center of mass: (%d,%d), Axis of Elongation: %lf , Perimeter: %lf, Thinness ratio: %lf, Aspect ratio: %lf \n", area, CenterOfMassX, CenterOfMassY, axisOfElongation, perimeter, T, R);
		mark.at<Vec3b>(CenterOfMassX, CenterOfMassY) = Vec3b(0, 0, 255);

		int length = -100;
		Point P1, P2;
		P1.x = (int)round(CenterOfMassY + length * cos(axisOfElongation * CV_PI / 180.0));
		P1.y = (int)round(CenterOfMassX + length * sin(axisOfElongation * CV_PI / 180.0));
		length *= -1;
		P2.x = (int)round(CenterOfMassY + length * cos(axisOfElongation * CV_PI / 180.0));
		P2.y = (int)round(CenterOfMassX + length * sin(axisOfElongation * CV_PI / 180.0));
		line(mark, P1, P2, COLORMAP_PINK, 1);
		/*for (int i = 0; i < (*src).rows; i++)
		for (int j = 0; j < (*src).cols; j++)
		{
		if((*src))

		}*/
		imshow("marked image", mark);
	}
}

void detectObjectDetailsOnMouseClick()
{
	Mat src;
	// Read image from file 
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		src = imread(fname);
		//Create a window
		namedWindow("My Window", 1);

		//set the callback function for any mouse event
		setMouseCallback("My Window", onMouse, &src);

		//show the image
		imshow("My Window", src);

		// Wait until user press some key
		waitKey(0);
	}
}

int labelMatrix[10000][10000];

void labelObject(int i, int j, int label, Mat img, int dircount)
{
	if (i < 0 || j < 0 || i >= img.rows || j >= img.cols)
		return;
	if (img.at<uchar>(i, j) != 255 && labelMatrix[i][j] == 0)
	{
		labelMatrix[i][j] = label;
		labelObject(i - 1, j, label, img, dircount);
		labelObject(i + 1, j, label, img, dircount);
		labelObject(i, j - 1, label, img, dircount);
		labelObject(i, j + 1, label, img, dircount);
		if (dircount == 8)
		{
			labelObject(i - 1, j - 1, label, img, dircount);
			labelObject(i + 1, j + 1, label, img, dircount);
			labelObject(i + 1, j - 1, label, img, dircount);
			labelObject(i - 1, j + 1, label, img, dircount);

		}
	}
}

void outputImageFromLabels(Mat img, int objects)
{

	default_random_engine gen;
	uniform_int_distribution<int> d(0, 255);
	Mat labeled(img.rows, img.cols, CV_8UC3);
	for (int o = 1; o <= objects; o++)
	{
		uchar x1 = d(gen);
		uchar x2 = d(gen);
		uchar x3 = d(gen);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
				if (labelMatrix[i][j] == o)
					labeled.at<Vec3b>(i, j)[0] = x1, labeled.at<Vec3b>(i, j)[1] = x2, labeled.at<Vec3b>(i, j)[2] = x3;
	}
	imshow("labels", labeled);

	waitKey(0);
}

void objectLabeling(int dircount, int algorithm)
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
				labelMatrix[i][j] = 0;

		int label = 0;
		if (algorithm == 1)
		{
			for (int i = 0; i < img.rows; i++)
				for (int j = 0; j < img.cols; j++)
				{
					if (img.at<uchar>(i, j) != 255 && labelMatrix[i][j] == 0)
					{
						label++;
						labelObject(i, j, label, img, dircount);
					}
				}
		}
		else
		{
			vector<vector<int>> edges;
			for (int i = 0; i < img.rows; i++)
			{
				vector<int> e;
				for (int j = 0; j < img.cols; j++)
					e.push_back(0);
				edges.push_back(e);
			}
			for (int i = 0; i < img.rows; i++)
				for (int j = 0; j < img.cols; j++)
					if (img.at<uchar>(i, j) != 255 && labelMatrix[i][j] == 0)
					{
						vector<int> L;
						if (labelMatrix[i - 1][j]) L.push_back(labelMatrix[i - 1][j]);
						if (labelMatrix[i + 1][j]) L.push_back(labelMatrix[i + 1][j]);
						if (labelMatrix[i][j - 1])L.push_back(labelMatrix[i][j - 1]);
						if (labelMatrix[i][j + 1])L.push_back(labelMatrix[i][j + 1]);
						if (dircount == 8)
						{

							if (labelMatrix[i - 1][j - 1])L.push_back(labelMatrix[i - 1][j - 1]);
							if (labelMatrix[i + 1][j + 1])L.push_back(labelMatrix[i + 1][j + 1]);
							if (labelMatrix[i + 1][j - 1])L.push_back(labelMatrix[i + 1][j - 1]);
							if (labelMatrix[i - 1][j + 1])L.push_back(labelMatrix[i - 1][j + 1]);
						}
						if (L.empty())
						{
							label++;
							labelMatrix[i][j] = label;
						}
						else
						{
							int x = L[0];
							for (int ii = 1; ii < L.size(); ii++)
								if (L[ii] < x)
									x = L[ii];
							labelMatrix[i][j] = x;
							for (int ii = 0; ii < L.size(); ii++)
								if (L[ii] != x)
								{
									edges[L[ii]].push_back(x);
									edges[x].push_back(L[ii]);
								}
						}
					}
			int newLabel = 0;
			int newLabels[10000];
			for (int i = 0; i <= label + 1; i++) newLabels[i] = 0;
			for (int i = 1; i <= label; i++)
			{
				if (newLabels[i] == 0)
				{
					newLabel++;
					queue<int> Q;
					newLabels[i] = newLabel;
					Q.push(i);
					while (!Q.empty())
					{
						int x = Q.front();
						Q.pop();
						for (int j = 0; j < edges[x].size(); j++)
							if (newLabels[edges[x][j]] == 0)
							{
								newLabels[edges[x][j]] = newLabel;
								Q.push(edges[x][j]);
							}
					}
				}
			}
			for (int i = 0; i < img.rows; i++)
				for (int j = 0; j < img.cols; j++)
					labelMatrix[i][j] = newLabels[labelMatrix[i][j]];
		}
		outputImageFromLabels(img, label);


	}
}

Point2i getStartPoint(Mat img) {
	for (int i = 0; i < img.rows; i++) {
		for (int j = 0; j < img.cols; j++) {
			if (img.at<uchar>(i, j) == 0)
				return Point2i(i, j);
		}
	}
}

Point2i getPixelAtDir(int dir, Point2i p) {
	int i = p.x;
	int j = p.y;
	switch (dir) {
	case 0:
		return Point2i(i, j + 1);
	case 1:
		return Point2i(i - 1, j + 1);
	case 2:
		return Point2i(i - 1, j);
	case 3:
		return Point2i(i - 1, j - 1);
	case 4:
		return Point2i(i, j - 1);
	case 5:
		return Point2i(i + 1, j - 1);
	case 6:
		return Point2i(i + 1, j);
	case 7:
		return Point2i(i + 1, j + 1);

	}

}

void contourDetection()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		imshow("original image", img);
		Mat copy(img.rows, img.cols, CV_8UC1);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
				copy.at<uchar>(i, j) = 255;


		Point2i startPoint = getStartPoint(img);
		Point2i currPoint = startPoint;
		Point2i nextPoint;
		vector<Point2i> border;
		int n = border.size();
		int dir = 7;
		border.push_back(startPoint);

		do {
			if (dir % 2 == 0)
				dir = (dir + 7) % 8;
			else
				dir = (dir + 6) % 8;

			nextPoint = getPixelAtDir(dir, currPoint);
			while (img.at<uchar>(nextPoint.x, nextPoint.y) == 255)
			{
				dir = (dir + 1) % 8;
				nextPoint = getPixelAtDir(dir, currPoint);
			}
			border.push_back(nextPoint);
			currPoint = nextPoint;

			n = border.size();
		} while (n <= 2 || (border.at(0) != border.at(n - 2) && border.at(1) != border.at(n - 1)));

		//colour copy image
		for (Point2i p : border)
			copy.at<uchar>(p.x, p.y) = 0;
		imshow("border", copy);
		waitKey(0);
	}
}

void generateChainCode()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		imshow("original image", img);
		Mat copy(img.rows, img.cols, CV_8UC1);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
				copy.at<uchar>(i, j) = 255;
		Point2i startPoint = getStartPoint(img);
		Point2i currPoint = startPoint;
		Point2i nextPoint;
		vector<Point2i> border;
		vector<int> code;
		vector<int> derivative;

		int n = border.size();
		int dir = 7;
		border.push_back(startPoint);

		do {
			if (dir % 2 == 0)
				dir = (dir + 7) % 8;
			else
				dir = (dir + 6) % 8;

			nextPoint = getPixelAtDir(dir, currPoint);
			while (img.at<uchar>(nextPoint.x, nextPoint.y) == 255)
			{
				dir = (dir + 1) % 8;
				nextPoint = getPixelAtDir(dir, currPoint);
			}

			border.push_back(nextPoint);
			code.push_back(dir);

			currPoint = nextPoint;

			n = border.size();
		} while (n <= 2 || (border.at(0) != border.at(n - 2) && border.at(1) != border.at(n - 1)));

		//colour copy image
		for (Point2i p : border)
			copy.at<uchar>(p.x, p.y) = 0;

		//output chaincode and compute derivative
		cout << "Chain code: \n";
		for (int i = 1; i < code.size(); i++)
		{
			derivative.push_back((code.at(i) - code.at(i - 1) + 8) % 8);
			cout << code.at(i) << " ";
		}

		std::cout << "\nDerivative: \n";
		for (int d : derivative)
			cout << d << " ";

		imshow("border", copy);
		waitKey(0);
	}
}

void rebuild() {
	int startx, starty, n;
	ifstream input;
	input.open("images/reconstruct.txt");
	input >> startx;
	input >> starty;
	input >> n;

	vector<int> code;
	vector<Point2i> border;
	Point2i start = Point2i(startx, starty);
	Point2i curr = start;
	for (int i = 0; i < n; i++) {
		int c;
		input >> c;
		code.push_back(c);
	}

	border.push_back(start);
	for (int i = 0; i < n; i++) {
		Point2i next = getPixelAtDir(code.at(i), curr);
		border.push_back(next);
		curr = next;
	}

	Mat img = imread("Images/gray_background.bmp", CV_LOAD_IMAGE_GRAYSCALE);
	imshow("original image", img);
	Mat copy = img.clone();

	for (Point p : border) {
		copy.at<uchar>(p.x, p.y) = 0;
	}
	imshow("border", copy);
	waitKey(0);
}



Mat dilatationOnce(Mat img)
{
	Mat copy(img.rows, img.cols, CV_8UC1);
	img.copyTo(copy);
	for (int i = 1; i < img.rows - 1; i++)
		for (int j = 1; j < img.cols - 1; j++)
		{
			if (img.at<uchar>(i, j) == 255)
				if (img.at<uchar>(i - 1, j) == 0 ||
					img.at<uchar>(i - 1, j - 1) == 0 ||
					img.at<uchar>(i, j - 1) == 0 ||
					img.at<uchar>(i + 1, j - 1) == 0 ||
					img.at<uchar>(i + 1, j) == 0 ||
					img.at<uchar>(i + 1, j + 1) == 0 ||
					img.at<uchar>(i, j + 1) == 0 ||
					img.at<uchar>(i - 1, j + 1) == 0)

					copy.at<uchar>(i, j) = 0;

		}
	return copy;
}


void dilatation(int n)
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		imshow("original image", img);
		Mat copy(img.rows, img.cols, CV_8UC1);
		img.copyTo(copy);
		while (n--)
			copy = dilatationOnce(copy);
		imshow("border", copy);
		waitKey(0);
	}
}

Mat erosionOnce(Mat img)
{
	Mat copy(img.rows, img.cols, CV_8UC1);
	img.copyTo(copy);
	for (int i = 1; i < img.rows - 1; i++)
		for (int j = 1; j < img.cols - 1; j++)
		{
			if (img.at<uchar>(i, j) == 0)
				if (img.at<uchar>(i - 1, j) == 255 ||
					img.at<uchar>(i - 1, j - 1) == 255 ||
					img.at<uchar>(i, j - 1) == 255 ||
					img.at<uchar>(i + 1, j - 1) == 255 ||
					img.at<uchar>(i + 1, j) == 255 ||
					img.at<uchar>(i + 1, j + 1) == 255 ||
					img.at<uchar>(i, j + 1) == 255 ||
					img.at<uchar>(i - 1, j + 1) == 255)

					copy.at<uchar>(i, j) = 255;

		}
	return copy;
}

void erosion(int n)
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		imshow("original image", img);
		Mat copy(img.rows, img.cols, CV_8UC1);
		img.copyTo(copy);
		while (n--)
			copy = erosionOnce(copy);
		imshow("border", copy);
		waitKey(0);
	}
}
Mat erosionGivenImage(Mat img, int n)
{
	Mat copy(img.rows, img.cols, CV_8UC1);
	img.copyTo(copy);
	while (n--)
		copy = erosionOnce(copy);
	return copy;
}


Mat dilatationGivenImage(Mat img, int n)
{
	Mat copy(img.rows, img.cols, CV_8UC1);
	img.copyTo(copy);
	while (n--)
		copy = dilatationOnce(copy);
	return copy;
}

void Opening(int n)
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		imshow("original image", img);
		Mat copy(img.rows, img.cols, CV_8UC1);
		img.copyTo(copy);
		copy = erosionGivenImage(copy, n);
		copy = dilatationGivenImage(copy, n);
		imshow("border", copy);
		waitKey(0);
	}
}
void Closing(int n)
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		imshow("original image", img);
		Mat copy(img.rows, img.cols, CV_8UC1);
		img.copyTo(copy);
		copy = dilatationGivenImage(copy, n);
		copy = erosionGivenImage(copy, n);
		imshow("border", copy);
		waitKey(0);
	}
}

void BoundryExtraction()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		imshow("original image", img);
		Mat copy(img.rows, img.cols, CV_8UC1);
		img.copyTo(copy);
		copy = erosionGivenImage(img, 1);
		for (int i = 1; i < img.rows - 1; i++)
			for (int j = 1; j < img.cols - 1; j++)
			{

				copy.at<uchar>(i, j) = img.at<uchar>(i, j) + 255 - copy.at<uchar>(i, j);
			}
		imshow("border", copy);
		waitKey(0);
	}
}
bool equalImages(Mat A, Mat B)
{

	for (int i = 0; i < A.rows; i++)
		for (int j = 0; j < A.cols; j++)
			if (A.at<uchar>(i, j) != B.at<uchar>(i, j))
				return false;
	return true;
}

Mat intersection(Mat A, Mat B)
{
	for (int i = 0; i < A.rows; i++)
		for (int j = 0; j < A.cols; j++)
			if (!(A.at<uchar>(i, j) == B.at<uchar>(i, j) && A.at<uchar>(i, j) == 0))
				A.at<uchar>(i, j) = 255;
	return A;
}


Mat unionImg(Mat A, Mat B)
{
	for (int i = 0; i < A.rows; i++)
		for (int j = 0; j < A.cols; j++)
			if (A.at<uchar>(i, j) == 255 && B.at<uchar>(i, j) == 255)
				A.at<uchar>(i, j) = 255;
			else
				A.at<uchar>(i, j) = 0;
	return A;
}
void regionFilling()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		imshow("original image", img);
		int px = img.rows / 2, py = img.cols / 2;//punct de start, trebuie schimbat (border crossing sau ceva)

		Mat neg(img.rows, img.cols, CV_8UC1);
		img.copyTo(neg);
		Mat aux(img.rows, img.cols, CV_8UC1);
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
			{
				neg.at<uchar>(i, j) = 255 - img.at<uchar>(i, j);
				aux.at<uchar>(i, j) = 255;
			}
		aux.at<uchar>(px, py) = 0;


		Mat rez(img.rows, img.cols, CV_8UC1);
		while (!equalImages(rez, aux))
		{
			aux.copyTo(rez);
			aux = dilatationGivenImage(aux, 1);
			aux = intersection(aux, neg);
		}
		imshow("border", unionImg(rez, img));
		waitKey(0);
	}
}
int histogram[256];
double pdf[256];
void ComputeHistogram(Mat img)
{
	for (int i = 0; i < img.rows; i++)
		for (int j = 0; j < img.cols; j++)
			histogram[(int)img.at<uchar>(i, j)]++;
	for (int i = 0; i <= 255; i++)
		pdf[i] = (double)histogram[i] / (img.rows*img.cols);
}
void MeanAndStandardDeviation()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		ComputeHistogram(img);
		double meanDeviation = 0, standardDeviation = 0;
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
				meanDeviation += (int)img.at<uchar>(i, j);
		meanDeviation /= (img.rows*img.cols);
		printf("mean deviation:%lf", meanDeviation);

		for (int i = 0; i <= 255; i++)
			standardDeviation += (i - meanDeviation)*(i - meanDeviation)*pdf[i];
		standardDeviation = sqrt(standardDeviation);
		printf("\nstandard deviation %lf", standardDeviation);
		system("pause");
	}
}
void HistogramAndPDF()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		ComputeHistogram(img);
		showHistogram("histogram drawing", histogram, 255, 200);
		waitKey(0);
	}
}
void threshComputation()
{
	double error = 0.1;
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		ComputeHistogram(img);
		double T1, T;
		int Imin = 256, Imax = 0;
		for (int i = 0; i <= 255; i++)
		{
			if (histogram[i] < histogram[Imin])
				Imin = i;
			if (histogram[i] > histogram[Imax])
				Imax = i;
		}
		T = (Imax + Imin) / 2;
		do {
			double mean1 = 0, mean2 = 0;
			int count1 = 0, count2 = 0;
			T1 = T;
			for (int i = 0; i <= 255; i++)
				if (i <= (int)T)
				{
					count1 += histogram[i];
					mean1 += i * histogram[i];
				}
				else
				{
					mean2 += i * histogram[i];
					count2 += histogram[i];
				}
			T = ((mean1 / count1) + (mean2 / count2)) / 2;
		} while (abs(T - T1) > error);
		printf("threshold: %lf", T);
		Threshold((int)T);
		waitKey(0);
	}
}

int newHistogram[256];
void histogramStrechShrink()
{
	char fname[MAX_PATH];
	if (openFileDlg(fname))
	{
		int OutMin, OutMax;
		int InMin = 256, InMax = 0;
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		Mat newImg(img.rows, img.cols, CV_8UC1);
		cout << "give min and max:";
		cin >> OutMin >> OutMax;
		ComputeHistogram(img);
		double T1, T;

		for (int i = 0; i <= 255; i++)
		{
			if (histogram[i] < histogram[InMin])
				InMin = i;
			if (histogram[i] > histogram[InMax])
				InMax = i;
		}
		for (int i = 0; i < img.rows; i++)
			for (int j = 0; j < img.cols; j++)
			{
				int val = img.at<uchar>(i, j);
				int newval = OutMin + (val - InMin) * ((OutMax - OutMin) / (InMax - InMin));
				newImg.at<uchar>(i, j) = (uchar)newval;
				newHistogram[newval] = histogram[val];
			}

		showHistogram("histogram drawing", histogram, 255, 200);

		showHistogram("new histogram drawing", newHistogram, 255, 200);
		imshow("new", newImg);
		waitKey(0);
	}
}
void equalizeHistogram() {
	Mat src;
	// Read image from file 
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		src = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		//Create a window
		namedWindow("Objects", 1);

		//show the image
		imshow("Objects", src);

		//compute histogram
		ComputeHistogram(src);
		showHistogram("Histogram", histogram, 256, 200);

		std::vector<int> cumulative_hist = std::vector<int>(256);
		int s = 0;
		for (int i = 0; i < 256; i++) {
			s += histogram[i];
			cumulative_hist.at(i) = s;
		}

		int* ch = &cumulative_hist[0];
		showHistogram("Cumulative Histogram", ch, 256, 200);

		int nr_pixels = src.rows * src.cols;
		std::vector<float> cpdf = std::vector<float>(256);
		for (int i = 0; i < 256; i++) {
			cpdf.at(i) = float(cumulative_hist.at(i)) / nr_pixels;
		}

		Mat newimg;
		src.copyTo(newimg);
		for (int i = 0; i < src.rows; i++) {
			for (int j = 0; j < src.cols; j++) {
				int val = src.at<uchar>(i, j);
				int gout = 255 * cpdf.at(val);
				newimg.at<uchar>(i, j) = gout;
			}
		}

		imshow("New image", newimg);
		waitKey(0);
	}
}


Mat_<uchar> pad(int k, Mat_<uchar> img)
{
	Mat copy(img.rows + k, img.cols + k, CV_8UC1);
	for (int i = 0; i < copy.rows; i++)
		for (int j = 0; j < copy.cols; j++)
			copy.at<uchar>(i, j) = 0;
	for (int i = 0; i < img.rows; i++)
		for (int j = 0; j < img.cols; j++)
			copy.at<uchar>(i + k / 2, j + k / 2) = img.at<uchar>(i, j);
	return copy;
}

void convolution(Mat_<float> &filter, Mat_<uchar> &img, Mat_<uchar> &output)
{
	output.create(img.size());
	output.setTo(0);
	float scalingCoeff = 1;
	float additionFactor = 0;
	int pos_elem = 0;
	int neg_elem = 0;
	float pos_sum = 0;
	float neg_sum = 0;
	for (int i = 0; i < filter.rows; i++)
	{
		for (int j = 0; j < filter.cols; j++)
		{
			if (filter.at<float>(i, j) >= 0)
			{
				pos_elem++;
				pos_sum += filter.at<float>(i, j);
			}
			else
			{
				neg_elem++;
				neg_sum += filter.at<float>(i, j);
			}
		}
	}
	if (pos_elem == filter.rows*filter.rows)
	{ //low pass
		additionFactor = 0;
		scalingCoeff = pos_sum;
	}
	else
	{ // highpass
		if (pos_sum > abs(neg_sum))
			scalingCoeff = 2 * pos_sum;
		else
			scalingCoeff = 2 * abs(neg_sum);
		additionFactor = 127;
	}
	int di[9] = { -1,-1,-1,0,0,0,1,1,1 };
	int dj[9] = { -1,0,1,-1,0,1,-1,0,1 };


	for (int i = filter.rows / 2; i < img.rows - filter.rows / 2; i++) {
		for (int j = filter.rows / 2; j < img.cols - filter.rows / 2; j++) {
			float sum = 0;
			for (int k = 0; k < filter.rows; k++)
				for (int l = 0; l < filter.cols; l++)
					sum += img(i + k - filter.rows / 2, j + l - filter.cols / 2) * filter(k, l);
			sum = min(abs(sum / scalingCoeff), 255);
			output(i, j) = sum;
		}
	}
}


void convolutionINT(Mat_<int> &filter, Mat_<uchar> &img, Mat_<int> &output)
{
	output.create(img.size());
	output.setTo(0);
	int kk = (filter.rows - 1) / 2;
	for (int i = kk; i < img.rows - kk; i++)
		for (int j = kk; j < img.cols - kk; j++)
		{
			float sum = 0;
			for (int k = 0; k < filter.rows; k++)
				for (int l = 0; l < filter.cols; l++)
					sum += img(i + k - kk, j + l - kk) * filter(k, l);
			//sum = min(abs(sum / scalingCoeff), 255);
			output(i, j) = sum;
		}

}

/*  in the frequency domain, the process of convolution simplifies to multiplication => faster than in the spatial domain
the output is simply given by F(u,v)ĂG(u,v) where F(u,v) and G(u,v) are the Fourier transforms of their respective functions
The frequency-domain representation of a signal carries information about the signal's magnitude and phase at each frequency*/

/*
The algorithm for filtering in the frequency domain is:
a) Perform the image centering transform on the original image (9.15)
b) Perform the DFT transform
c) Alter the Fourier coefficients according to the required filtering
d) Perform the IDFT transform
e) Perform the image centering transform again (this undoes the first centering transform)
*/

void centering_transform(Mat img)
{
	//expects floating point image
	for (int i = 0; i < img.rows; i++)
		for (int j = 0; j < img.cols; j++)
			img.at<float>(i, j) = ((i + j) & 1) ? -img.at<float>(i, j) : img.at<float>(i, j);
}

Mat generic_frequency_domain_filter(Mat src)
{
	int height = src.rows;
	int width = src.cols;

	Mat srcf;
	src.convertTo(srcf, CV_32FC1);
	// Centering transformation
	centering_transform(srcf);

	//perform forward transform with complex image output
	Mat fourier;
	dft(srcf, fourier, DFT_COMPLEX_OUTPUT);

	//split into real and imaginary channels fourier(i, j) = Re(i, j) + i * Im(i, j)
	Mat channels[] = { Mat::zeros(src.size(), CV_32F), Mat::zeros(src.size(), CV_32F) };
	split(fourier, channels);  // channels[0] = Re (real part), channels[1] = Im (imaginary part)

							   //calculate magnitude and phase in floating point images mag and phi
							   // http://www3.ncc.edu/faculty/ens/schoenf/elt115/complex.html
							   // from cartesian to polar coordinates

	Mat mag, phi;
	magnitude(channels[0], channels[1], mag);
	phase(channels[0], channels[1], phi);


	// TODO: Display here the log of magnitude (Add 1 to the magnitude to avoid log(0)) (see image 9.4e))
	// do not forget to normalize
	Mat out;
	log(mag + 1, out);
	normalize(out, out, 0, 1, CV_MINMAX);
	imshow("mag log", out);

	// TODO: Insert filtering operations here ( channels[0] = Re(DFT(I), channels[1] = Im(DFT(I) )


	//lowpass
	for (int i = 0; i < src.rows; i++) {
		for (int j = 0; j < src.cols; j++) {
			if (pow((src.rows / 2 - i), 2) + pow((src.cols / 2 - j), 2) > 400) {
				channels[0].at<uchar>(i, j) = 0;
				channels[1].at<uchar>(i, j) = 0;
			}
		}
	}


	//perform inverse transform and put results in dstf
	Mat dst, dstf;
	merge(channels, 2, fourier);
	dft(fourier, dstf, DFT_INVERSE | DFT_REAL_OUTPUT);

	// Inverse Centering transformation
	centering_transform(dstf);

	//normalize the result and put in the destination image
	normalize(dstf, dst, 0, 255, NORM_MINMAX, CV_8UC1);

	return dst;
}

void mean_filter()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat_<uchar> img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		float meanFilterData5x5[25];
		std::fill_n(meanFilterData5x5, 25, 1);
		Mat_<float> meanFilter3x3(3, 3, meanFilterData5x5);
		Mat_<uchar> out;
		convolution(meanFilter3x3, img, out);

		imshow("new", out);
		waitKey(0);
	}
}

void gaussian_filter()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat_<uchar> img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		float gaussianFilterData[9] = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
		Mat_<float> gaussianFilter(3, 3, gaussianFilterData);
		Mat_<uchar> out;
		convolution(gaussianFilter, img, out);

		imshow("new", out);
		waitKey(0);
	}
}

void laplace_filter()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat_<uchar> img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		float laplaceFilterData[9] = { -1, -1, -1, -1, 8, -1, -1, -1, -1 };
		Mat_<float> laplaceFilter(3, 3, laplaceFilterData);
		Mat_<uchar> out;
		convolution(laplaceFilter, img, out);

		imshow("new", out);
		waitKey(0);
	}
}

void highpass_filter()
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat_<uchar> img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		float highpassFilterData[9] = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
		Mat_<float> highpassFilter(3, 3, highpassFilterData);
		Mat_<uchar> out;
		convolution(highpassFilter, img, out);

		imshow("new", out);
		waitKey(0);
	}
}

void median_filter(int dimension)
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		img = pad(dimension, img);
		imshow("before filter", img);
		Mat img_filtered;
		img.copyTo(img_filtered);
		for (int i = dimension / 2; i < img.rows - dimension / 2; i++)
			for (int j = dimension / 2; j < img.cols - dimension / 2; j++)
			{
				Vector<int> filter_elements;
				for (int k = 0; k < dimension; k++)
					for (int l = 0; l < dimension; l++)
						filter_elements.push_back(img.at<uchar>(i + k - dimension / 2, j + l - dimension / 2));
				std::sort(filter_elements.begin(), filter_elements.end());
				img_filtered.at<uchar>(i, j) = filter_elements[dimension*dimension / 2];
			}

		imshow("filtered", img_filtered);
		waitKey(0);
	}
}

void gaussianFilter(int dimension)
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat_<uchar> img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		img = pad(dimension, img);
		Mat_<uchar> img_filtered;
		img.copyTo(img_filtered);
		imshow("before filter", img);
		float sigma = dimension / 6.0;
		Mat_<float> filter(dimension, dimension);
		for (int i = 0; i < dimension; i++)
			for (int j = 0; j < dimension; j++)
				filter(i, j) = exp(-(pow(i - dimension / 2, 2) + pow(j - dimension / 2, 2)) / (2 * sigma*sigma)) / (2 * PI*sigma*sigma);
		convolution(filter, img, img_filtered);
		imshow("filtered", img_filtered);
		waitKey(0);
	}
}

void gaussianFilterCross(int dimension)
{
	char fname[MAX_PATH];
	while (openFileDlg(fname))
	{
		Mat_<uchar> img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
		Mat_<float> filterY, filterX;
		Mat_<uchar> img_filtered;
		Mat_<float> filter(dimension, dimension);
		img = pad(dimension, img);
		imshow("before filter", img);
		float sigma = dimension / 6.0;
		for (int i = 0; i < dimension; i++)
			for (int j = 0; j < dimension; j++)
				filter(i, j) = exp(-(pow(i - dimension / 2, 2) + pow(j - dimension / 2, 2)) / (2 * sigma*sigma)) / (2 * PI*sigma*sigma);
		filter.copyTo(filterY);
		filter.copyTo(filterX);
		for (int i = 0; i < dimension; i++)
			filterY(i, dimension / 2) = filterX(dimension / 2, i) = 0;
		convolution(filterY, img, img_filtered);
		convolution(filterX, img_filtered, img_filtered);
		imshow("filtered", img_filtered);
		waitKey(0);
	}
}

Mat Sobelx(Mat src)
{
	Mat_<int> m = (Mat_<int>(3, 3) << -1, 0, 1, -2, 0, 2, -1, 0, 1);
	Mat_<int> ret;
	convolutionINT(m, (Mat_<uchar>)src, ret);
	return ret;
}

Mat Sobely(Mat src)
{
	Mat_<int> m = (Mat_<int>(3, 3) << 1, 2, 1, 0, 0, 0, -1, -2, -1);
	Mat_<int> ret;
	convolutionINT(m, (Mat_<uchar>)src, ret);
	return ret;
}

void HorisontalVerticalComponents(Mat img)
{
	Mat x = Sobelx(img);
	Mat y = Sobely(img);
	imshow("x", x);
	imshow("y", y);
}


Mat getEdges(Mat src)
{
	Mat_<uchar> dest = Mat(src.rows, src.cols, CV_8UC1, Scalar(0));
	Mat_<uchar> orientation = Mat(src.rows, src.cols, CV_8UC1, Scalar(0));
	Mat_<uchar> magnitude = Mat(src.rows, src.cols, CV_8UC1, Scalar(0));

	Mat_<int> x = Sobelx(src);
	Mat_<int> y = Sobely(src);


	int maxi = -500, mini = 500;
	for (int i = 0; i < src.rows; i++)
		for (int j = 0; j < src.cols; j++)
		{
			if (x.at<int>(i, j) < mini)
				mini = x.at<int>(i, j);
			if (x.at<int>(i, j) > maxi)
				maxi = x.at<int>(i, j);

		}
	cout << "dfgsdfbxcdxbdfx  " << mini << " " << maxi;


	for (int i = 0; i < src.rows; i++)
		for (int j = 0; j < src.cols; j++)
		{
			uchar res = sqrt(x.at<int>(i, j) * x.at<int>(i, j) + y.at<int>(i, j) * y.at<int>(i, j)) / (4 * sqrt(2));
			magnitude.at<uchar>(i, j) = res;
			double at = atan2(y.at<int>(i, j), x.at<int>(i, j));
			if (at < 0)
				at = at + PI;
			if (at < PI / 8 || at >= 7 * PI / 8)
				orientation.at<uchar>(i, j) = 1;
			else if (at >= PI / 8 && at < 3 * PI / 8)
				orientation.at<uchar>(i, j) = 2;
			else if (at >= 3 * PI / 8 && at < 5 * PI / 8)
				orientation.at<uchar>(i, j) = 3;
			else if (at >= 5 * PI / 8 && at < 7 * PI / 8)
				orientation.at<uchar>(i, j) = 4;

		}
	imshow("magnitude before non maxima", magnitude);

	for (int r = 1; r < src.rows - 1; r++)
		for (int c = 1; c < src.cols - 1; c++)
		{
			dest[r][c] = magnitude[r][c];
			switch (orientation[r][c])
			{
			case 1:
				if (magnitude[r][c] < magnitude[r][c + 1] || magnitude[r][c] < magnitude[r][c - 1])
					dest[r][c] = 0;
				break;
			case 2:
				if (magnitude[r][c] < magnitude[r - 1][c + 1] || magnitude[r][c] < magnitude[r + 1][c - 1])
					dest[r][c] = 0;
				break;
			case 3:
				if (magnitude[r][c] < magnitude[r - 1][c] || magnitude[r][c] < magnitude[r + 1][c])
					dest[r][c] = 0;
				break;
			case 4:
				if (magnitude[r][c] < magnitude[r + 1][c + 1] || magnitude[r][c] < magnitude[r - 1][c - 1])
					dest[r][c] = 0;
				break;
			}
		}
	return dest;
}

void fillWithWhite(Mat result, Mat src, Mat visited, int i, int j)
{
	if (i >= 0 && j >= 0 && i < src.rows && j < src.cols)
		if (src.at<uchar>(i, j) != 0 && visited.at<uchar>(i, j) == 0)
		{
			visited.at<uchar>(i, j) = 255;
			result.at<uchar>(i, j) = 255;
			fillWithWhite(result, src, visited, i + 1, j);
			fillWithWhite(result, src, visited, i - 1, j);
			fillWithWhite(result, src, visited, i, j + 1);
			fillWithWhite(result, src, visited, i, j - 1);
			fillWithWhite(result, src, visited, i + 1, j - 1);
			fillWithWhite(result, src, visited, i - 1, j - 1);
			fillWithWhite(result, src, visited, i + 1, j + 1);
			fillWithWhite(result, src, visited, i - 1, j + 1);
		}
}


void fillWithBlack(Mat result, Mat src, Mat visited, int i, int j)
{
	if (i >= 0 && j >= 0 && i < src.rows && j < src.cols)
		if (src.at<uchar>(i, j) != 0 && visited.at<uchar>(i, j) == 0)
		{
			visited.at<uchar>(i, j) = 255;
			result.at<uchar>(i, j) = 0;
			fillWithBlack(result, src, visited, i + 1, j);
			fillWithBlack(result, src, visited, i - 1, j);
			fillWithBlack(result, src, visited, i, j + 1);
			fillWithBlack(result, src, visited, i, j - 1);
			fillWithBlack(result, src, visited, i + 1, j - 1);
			fillWithBlack(result, src, visited, i - 1, j - 1);
			fillWithBlack(result, src, visited, i + 1, j + 1);
			fillWithBlack(result, src, visited, i - 1, j + 1);
		}
}


Mat fillGrey(Mat src)
{
	Mat_<uchar> result = Mat(src.rows, src.cols, CV_8UC1, Scalar(0));
	Mat_<uchar> visited = Mat(src.rows, src.cols, CV_8UC1, Scalar(0));
	for (int i = 0; i < src.rows; i++)
		for (int j = 0; j < src.cols; j++)
			if (visited.at<uchar>(i, j) == 0 && src.at<uchar>(i, j) == 255)
				fillWithWhite(result, src, visited, i, j);

	for (int i = 0; i < src.rows; i++)
		for (int j = 0; j < src.cols; j++)
			if (visited.at<uchar>(i, j) == 0 && src.at<uchar>(i, j) == 128)
				fillWithBlack(result, src, visited, i, j);
	imshow("visited", visited);
	return result;
}

Mat getEdgesAdaptiveThresholding(Mat src, int percent)
{
	int Thigh = 0, Tlow;
	Mat_<uchar> magnitude = getEdges(src);
	imshow("magnitude", magnitude);
	Mat_<uchar> threeColourMat = Mat(src.rows, src.cols, CV_8UC1, Scalar(0));
	int histo[256];
	for (int i = 0; i <= 255; i++)
		histo[i] = 0;
	for (int i = 0; i < magnitude.rows; i++)
		for (int j = 0; j < magnitude.cols; j++)
			histo[(int)magnitude.at<uchar>(i, j)]++;
	//for (int i = 0; i <= 255; i++)
	//	cout<<histo[i]<<' ';
	int nonEdgePixels = (1 - percent / 100)*((magnitude.rows - 2)*(magnitude.cols - 2) - histo[0]);
	//cout << nonEdgePixels << " ";
	int sum = 0;
	for (Thigh = 1; Thigh <= 255 && sum <= nonEdgePixels; Thigh++)
		sum += histo[Thigh];
	Tlow = 0.4*Thigh;
	//cout << Tlow << " " << Thigh;
	for (int i = 0; i < magnitude.rows; i++)
		for (int j = 0; j < magnitude.cols; j++)
		{
			if (magnitude.at<uchar>(i, j) > Thigh)
				threeColourMat.at<uchar>(i, j) = 255;
			else if (magnitude.at<uchar>(i, j) > Tlow)
				threeColourMat.at<uchar>(i, j) = 128;
			else
				threeColourMat.at<uchar>(i, j) = 0;
		}
	imshow("intermediate", threeColourMat);
	return fillGrey(threeColourMat);
}



static void help()
{
	cout << "\nHot keys: \n"
		"\tESC - quit the program\n"
		"\tr - auto-initialize tracking\n"
		"\tc - delete all the points\n"
		"\tn - switch the \"night\" mode on/off\n"
		"To add/remove a feature point click it\n" << endl;
}

Point2f clickPoint;
bool addRemovePt = false;

static void onMouse1(int event, int x, int y, int flags, void* param)
{
	if (event == EVENT_LBUTTONDOWN)
	{
		clickPoint = Point2f((float)x, (float)y);
		addRemovePt = true;
	}
}



void LukasKanadeOpenCV()
{
	cv::VideoCapture cap(0);
	TermCriteria termcrit(TermCriteria::COUNT | TermCriteria::EPS, 20, 0.03);
	Size subPixWinSize(10, 10), winSize(31, 31);

	const int MAX_COUNT = 500;
	bool needToInit = false;
	bool nightMode = false;

	help();

	if (!cap.isOpened())
	{
		cout << "Could not initialize capturing...\n";
		return;
	}

	namedWindow("LK optical flow", 1);
	setMouseCallback("LK optical flow", onMouse1, 0);

	Mat gray, prevGray, image, frame;
	vector<Point2f> points[2];

	for (;;)
	{
		cap >> frame;
		if (frame.empty())
			break;

		frame.copyTo(image);
		cvtColor(image, gray, COLOR_BGR2GRAY);

		if (nightMode)
			image = Scalar::all(0);

		if (needToInit)
		{
			// automatic initialization
			goodFeaturesToTrack(gray, points[1], MAX_COUNT, 0.01, 10, Mat(), 3, 0, 0.04);
			cornerSubPix(gray, points[1], subPixWinSize, Size(-1, -1), termcrit);
			addRemovePt = false;
		}

		else if (!points[0].empty())
		{
			vector<uchar> status;
			vector<float> err;
			if (prevGray.empty())
				gray.copyTo(prevGray);
			calcOpticalFlowPyrLK(prevGray, gray, points[0], points[1], status, err, winSize, 3, termcrit, 0, 0.001);
			size_t i, k;
			for (i = k = 0; i < points[1].size(); i++)
			{
				//sterge punctele care dispar
				if (!status[i])
					continue;
				points[1][k++] = points[1][i];
				circle(image, points[1][i], 3, Scalar(255, 0, 0), -1);
			}
			points[1].resize(k);

			for (int i = 0; i < min(points[0].size(), points[1].size()); i++)
				if ((points[0][i].x - points[1][i].x)*(points[0][i].x - points[1][i].x)
					+ (points[0][i].y - points[1][i].y)*(points[0][i].y - points[1][i].y) < 1000)
					line(image, Point(points[0][i].x, points[0][i].y), Point(points[1][i].x, points[1][i].y), Scalar(0, 0, 255), 1);
		}

		needToInit = false;
		imshow("LK flow", image);

		char c = (char)waitKey(10);
		if (c == 27)
			break;
		switch (c)
		{
		case 'r':
			needToInit = true;
			break;
		case 'c':
			points[0].clear();
			points[1].clear();
			break;
		case 'n':
			nightMode = !nightMode;
			break;
		default:
			break;
		}
		std::swap(points[1], points[0]);
		cv::swap(prevGray, gray);
	}
}



typedef struct { int x, y; } point;



#define PATCH_RADIUS 8 //L-K mask

static int IMW = 0;
static int IMH = 0;
static unsigned char* previous_src = NULL;
static float* Ix = NULL;
static float* Iy = NULL;
static float* It = NULL;

void LukasKanade(const unsigned char* src, const point* corner_start, const int NUMCORNERS, point* corner_end)
{
#pragma region  definotions
	int whole_idx = 0;
	int previous_ix = 0;
	int previous_iy = 0;
	int corner_idx = 0;
	float Vx = 0.0f, Vy = 0.0f;
#pragma endregion definitions
	//Compute the partial derivatives Ix Iy It for each point 
	for (int y = 0; y < IMH; ++y)
		for (int x = 0; x < IMW; ++x)
		{
			// 0 -1  0
			//-1  0  1
			// 0  1  0
			whole_idx = y * IMW + x;
			int ix = src[whole_idx + 1] - src[whole_idx - 1];
			previous_ix = previous_src[whole_idx + 1] - previous_src[whole_idx - 1];
			int iy = src[whole_idx + IMW] - src[whole_idx - IMW];
			previous_iy = previous_src[whole_idx + IMW] - previous_src[whole_idx - IMW];
			Ix[whole_idx] = (ix + previous_ix) * 0.5f;
			Iy[whole_idx] = (iy + previous_iy) * 0.5f;
			It[whole_idx] = (float)(src[whole_idx] - previous_src[whole_idx]);
		}

	for (corner_idx = 0; corner_idx < NUMCORNERS; ++corner_idx)
	{
		float sumIxIx = 0, sumIxIy = 0, sumIyIy = 0, sumIxIt = 0, sumIyIt = 0;
		//build the system v=(At*A)^-1*At*b (after the manual formula description)
		for (int y = -PATCH_RADIUS; y <= PATCH_RADIUS; y++)
			for (int x = -PATCH_RADIUS; x <= PATCH_RADIUS; x++)
			{
				whole_idx = (min((max(corner_start[corner_idx].y + y, 0)), IMH) * IMW)
					+ min((max(corner_start[corner_idx].x + x, 0)), IMW);
				sumIxIx += Ix[whole_idx] * Ix[whole_idx];
				sumIyIy += Iy[whole_idx] * Iy[whole_idx];
				sumIxIy += Ix[whole_idx] * Iy[whole_idx];
				sumIxIt += Ix[whole_idx] * It[whole_idx];
				sumIyIt += Iy[whole_idx] * It[whole_idx];
			}
		float determinant = sumIxIx * sumIyIy - sumIxIy * sumIxIy;
		//if A^T*A is invertible, compute Vx and Vy
		if (determinant != 0.0f)
		{
			float determinant_inverse = 1.0f / determinant;
			Vx = (sumIxIy * sumIyIt - sumIyIy * sumIxIt) * determinant_inverse;
			Vy = (sumIxIy * sumIxIt - sumIxIx * sumIyIt) * determinant_inverse;
		}
		//if vector is too small make increase it
		if (PATCH_RADIUS > fabs(Vx) && PATCH_RADIUS > fabs(Vy) && 0.05f < fabs(Vx) && 0.05f < fabs(Vy))
		{
			corner_end[corner_idx].x -= (int)(Vx * 5.0f);
			corner_end[corner_idx].y -= (int)(Vy * 5.0f);
		}
	}
	//save frame
	memcpy(previous_src, src, IMW * IMH * sizeof(unsigned char));
}

int xarea = 8, yarea = 8, thres = 3; //x and  y dimensions of a patch, the threshold that decides if a point is of interest or not
point* findCorners(Mat img, int &cornerCount)// Morevac Corner Detection 
{
	point* corners = (point*)malloc(500 * sizeof(point));//building an array of corners
	Mat outimg = img.clone();
	int dimx = img.cols, dimy = img.rows;
	int count = 0;
	for (int startx = 0; (startx + xarea) < dimx; startx += xarea)
		for (int starty = 0; (starty + yarea) < dimy; starty += yarea)
		{
			count++;
			Mat curarea = img(Range(starty, min(starty + yarea, dimy)), Range(startx, min(dimx, startx + xarea)));
			double results[2] = { 0,0 };
			for (int dir = 0; dir < 4; dir++)
			{
				int newsx = startx, newsy = starty;
				//Check similarity in each direction
				switch (dir)
				{
				case 0: //left
					newsx -= xarea;
					newsx = max(newsx, 0);
					break;
				case 1: //top
					newsy -= yarea;
					newsy = max(newsy, 0);
					break;
				case 2: //right
					newsx += xarea;
					newsx = min(newsx, dimx);
					break;
				case 3: //down
					newsy += yarea;
					newsy = min(newsy, dimy);
					break;
				default:
					break;
				}
				Mat newarea = img(Range(newsy, min(newsy + yarea, dimy)), Range(newsx, min(newsx + xarea, dimx)));
				Mat diff = abs(curarea - newarea);
				results[dir % 2] = mean(mean(diff))(0);
			}
			if (results[0] >= 2 * thres && results[1] >= 2 * thres)
			{
				point c;
				c.x = startx;
				c.y = starty;
				corners[cornerCount++] = c;
			}
		}
	return corners;
}

void LukasKanadeOpenCustom()
{
	Mat im;
	Mat gray;
	VideoCapture cap;
	cap.open(0);
	if (!cap.isOpened())
	{
		cout << "Failed to open cam" << endl;
		exit(EXIT_FAILURE);
	}
	cap >> im;
	cvtColor(im, gray, CV_BGR2GRAY);
	//initialize variable
	IMW = gray.cols;
	IMH = gray.rows;
	previous_src = (unsigned char*)malloc(IMW * IMH * sizeof(unsigned char));
	memcpy(previous_src, gray.data, IMW * IMH * sizeof(unsigned char));
	Ix = (float*)malloc(IMW * IMH * sizeof(float));
	Iy = (float*)malloc(IMW * IMH * sizeof(float));
	It = (float*)malloc(IMW * IMH * sizeof(float));
	while (1)
	{
		cap >> im;
		cvtColor(im, gray, CV_BGR2GRAY);
		int numcorners = 0;
		int i = 0;
		const int CORNER_THRESHOLD = 15;
		point* corners_start = findCorners(gray, numcorners);
		point* corners_end = (point*)malloc(numcorners * sizeof(point));
		memcpy(corners_end, corners_start, numcorners * sizeof(point));
		LukasKanade(gray.data, corners_start, numcorners, corners_end);
		//draw result

		for (i = 0; i < numcorners; ++i)
		{
			circle(im, Point(corners_start[i].x, corners_start[i].y), 2, Scalar(255, 0, 0), -1);
			line(im, Point(corners_start[i].x, corners_start[i].y),
				Point(corners_end[i].x, corners_end[i].y), Scalar(0, 0, 255), 1);
		}
		imshow("im", im);
		if (waitKey(10) == 27)
		{
			imwrite("flow.jpg", im);
			break;
		}
		free(corners_start);
		free(corners_end);
	}
	//free variablefree(previous_src);
	free(Ix);
	free(Iy);
	free(It);
}
int main()
{
	int op;
	do
	{
		system("cls");
		destroyAllWindows();
		printf("Menu:\n");
		printf(" 1 - Open image\n");
		printf(" 2 - Open BMP images from folder\n");
		printf(" 3 - Resize image\n");
		printf(" 4 - Process video\n");
		printf(" 5 - Snap frame from live video\n");
		printf(" 6 - Mouse callback demo\n");
		printf(" 7 - Negative image\n");
		printf(" 8 - Aditive image\n");
		printf(" 9 - Multiplicative image\n");
		printf(" 10 - Square\n");
		printf(" 11 - Flip horisontally\n");
		printf(" 12 - Flip vertically\n");
		printf(" 13 - Center Crop\n");
		printf(" 14 - Separate Colors\n");
		printf(" 15 - Grayscale\n");
		printf(" 16 - Threshold\n");
		printf(" 17 - Convert to HSV\n");
		printf(" 18 - Detect Sign\n");
		printf(" 19 - Detect Details of an object\n");
		printf(" 20 - object labeling\n");
		printf(" 21 - contour detection\n");
		printf(" 22 - generate chain code\n");
		printf(" 23 - rebuild using chain code\n");
		printf(" 24 - dilatation\n");
		printf(" 25 - erosion\n");
		printf(" 26 - Opening\n");
		printf(" 27 - Closing\n");
		printf(" 28 - Boundry Extraction\n");
		printf(" 29 - Region fillig\n");
		printf(" 30 - Compute and display the mean and standard deviation of image intensity levels\n");
		printf(" 31 - Compute the histogram and probability distribution function of an image.\n");
		printf(" 32 - Implement the automatic threshold computation and threshold the images according to this threshold. (use error = 0.1)\n");
		printf(" 33 - stretching/shrinking histogram\n");
		printf(" 34 - equalize histogram\n");
		printf(" 35 - Mean filter\n");
		printf(" 36 - Gaussian filter\n");
		printf(" 37 - Laplace filter\n");
		printf(" 38 - Highpass filter\n");
		printf(" 39 - Centering transform\n");
		printf(" 40 - Median filter\n");
		printf(" 41 - Gaussian filter\n");
		printf(" 42 - Gaussian filter XY\n");
		printf(" 43 -The horizontal and vertical components of the gradient through convolution with the kernelsXY\n");
		printf(" 44 - Edge detection XY\n");
		printf(" 45 - Edge detection Adaptive tresholding\n");
		printf(" 100 - Lukas Kanade optical flow algorithm\n");
		printf(" 0 - Exit\n\n");
		printf("Option: ");
		scanf("%d", &op);
		switch (op)
		{
		case 1:
			testOpenImage();
			break;
		case 2:
			testOpenImagesFld();
			break;
		case 3:
			testResize();
			break;
		case 4:
			testVideoSequence();
			break;
		case 5:
			testSnap();
			break;
		case 6:
			testMouseClick();
			break;
		case 7:
			negative_image();
			break;
		case 8:
			printf("\n\n Give factor:\n");
			int factor_add;
			cin >> factor_add;
			modify_add(factor_add);
			break;
		case 9:
			printf("\n\n Give factor:\n");
			float factor_mul;
			cin >> factor_mul;
			modify_mul(factor_mul);
			break;
		case 10:
			draw_square();
			break;
		case 11:
			h_flip();
			break;
		case 12:
			v_flip();
			break;
		case 13:
			int w, h;
			printf("give width and hight of crop:");
			cin >> w >> h;
			center_crop(w, h);
			break;
		case 14:
			separate_colors();
			break;
		case 15:
			grayscale();
			break;
		case 16:
			int th;
			printf("give threshold:");
			cin >> th;
			Threshold(th);
			break;
		case 17:
			convertToHSV();
			break;
		case 18:
			detectSign();
			break;
		case 19:
			detectObjectDetailsOnMouseClick();
			break;
		case 20:
			printf("give number of directions (4 or 8):");
			cin >> th;
			int alg;
			printf("chose algorithm 1 for fill 2 for 2-pass:");
			cin >> alg;
			objectLabeling(th, alg);
			break;
		case 21:
			contourDetection();
			break;
		case 22:
			generateChainCode();
			break;
		case 23:
			rebuild();
			break;
		case 24:
			int n;
			printf("chose number of dilatations:");
			cin >> n;
			dilatation(n);
			break;
		case 25:
			printf("chose number of erosions:");
			cin >> n;
			erosion(n);
			break;
		case 26:
			printf("chose number of erosions and dilatations:");
			cin >> n;
			Opening(n);
			break;
		case 27:
			printf("chose number of erosions and dilatations:");
			cin >> n;
			Closing(n);
			break;
		case 28:
			BoundryExtraction();
			break;
		case 29:
			regionFilling();
			break;
		case 30:
			MeanAndStandardDeviation();
			break;
		case 31:
			HistogramAndPDF();
			break;
		case 32:
			threshComputation();
			break;
		case 33:
			histogramStrechShrink();
			break;
		case 34:
			equalizeHistogram();
			break;
		case 35:
			mean_filter();
			break;
		case 36:
			gaussian_filter();
			break;
		case 37:
			laplace_filter();
			break;
		case 38:
			highpass_filter();
			break;
		case 39:
			char fname[MAX_PATH];
			while (openFileDlg(fname))
			{
				Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
				imshow("new", generic_frequency_domain_filter(img));
				waitKey(0);
			}
			break;
		case 40:
			int dimension;
			std::cout << "\nDimension = ";
			std::cin >> dimension;
			std::cout << "\n";
			median_filter(dimension);
			break;
		case 41:
			int dimension2;
			std::cout << "\nDimension = ";
			std::cin >> dimension2;
			std::cout << "\n";
			gaussianFilter(dimension2);
			break;
		case 42:
			int dimension3;
			std::cout << "\nDimension = ";
			std::cin >> dimension3;
			std::cout << "\n";
			gaussianFilterCross(dimension3);
			break;
		case 43:
			while (openFileDlg(fname))
			{
				Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
				HorisontalVerticalComponents(img);
				waitKey(0);
			}
			break;
		case 44:
			while (openFileDlg(fname))
			{
				Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
				imshow("image", img);
				Mat edges = getEdges(img);
				imshow("edges", edges);
				waitKey(0);
			}
			break;
		case 45:
			while (openFileDlg(fname))
			{
				Mat img = imread(fname, CV_LOAD_IMAGE_GRAYSCALE);
				imshow("image", img);
				//cout << "give percentage for strong edge pixels:";
				int percentage = 10;
				//cin >> percentage;
				Mat edges = getEdgesAdaptiveThresholding(img, percentage);
				imshow("edges", edges);
				waitKey(0);
			}
			break;
		case 100:
			int c;
			cout << "Press 1 for the OpcenCV implementation or 2 for the custom implementation:";
			cin >> c;
			if (c == 1)
				LukasKanadeOpenCV();
			else
				LukasKanadeOpenCustom();
			break;
		}
	} while (op != 0);
	return 0;
}